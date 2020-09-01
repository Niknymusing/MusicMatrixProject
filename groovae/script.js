let grooveModel;
let mvae;

let modelSequence;

init();

function init() {
  grooveModel = new mm.MusicVAE('https://storage.googleapis.com/magentadata/js/checkpoints/music_vae/groovae_2bar_humanize'); 
  mvae = new mm.MusicVAE('https://storage.googleapis.com/magentadata/js/checkpoints/music_vae/drums_2bar_lokl_small'); 
  
  mvae.initialize();

  grooveModel.initialize().then(() => {
    drumifyControls.removeAttribute('disabled');
    btnDrumify.textContent = 'groove and send midi';
  });
  connect();
}

async function groove() {

  const sequence = notes;
  const temp = parseFloat(1.0);
  
  const z = await grooveModel.encode([sequence]);
  const recon = await grooveModel.decode(z, temp, undefined, 4, parseInt(inputTempo.value));
  modelSequence = recon[0];
  console.log(modelSequence);

  z.dispose();

  sendGrooveToMidi()
}


function connect() {
  navigator.requestMIDIAccess()
  .then(
    (midi) => midiReady(midi),
    (err) => console.log('Something went wrong', err));
}

function midiReady(midi) {
  midi.addEventListener('statechange', (event) => initDevices(event.target));
  initDevices(midi);
}

function initDevices(midi) {
    midiOut = [];
    
    const outputs = midi.outputs.values();
    for (let output = outputs.next(); output && !output.done; output = outputs.next()) {
      midiOut.push(output.value);
    }
  }
  

  function sendMidiMessage(pitch, velocity, duration) {
    const NOTE_ON = 0x90;
    const NOTE_OFF = 0x80;
    
    const device = midiOut[0];
    const msgOn = [NOTE_ON, pitch, velocity];
    const msgOff = [NOTE_OFF, pitch, velocity];
    
    device.send(msgOn); 
    device.send(msgOff, Date.now() + duration); 
  }

  function sendGrooveToMidi(){
     for (let note of modelSequence.notes){
        console.log(note)
        sendMidiMessage(note.pitch, note.velocity, 100)
     }
  }