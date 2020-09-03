let grooveModel;
let mvae;

let modelSequence;

init();

function init() {
  grooveModel = new mm.MusicVAE('https://storage.googleapis.com/magentadata/js/checkpoints/music_vae/groovae_2bar_humanize');

  grooveModel.initialize().then(() => {
    drumifyControls.removeAttribute('disabled');
    btnDrumify.textContent = 'groove and send midi';
  });
}

async function groove() {

  const sequence = notes;
  const temp = parseFloat(1.0);

  const z = await grooveModel.encode([sequence]);
  const recon = await grooveModel.decode(z, temp, undefined, 4, parseInt(inputTempo.value));
  modelSequence = recon[0];

  z.dispose();

  sendGrooveToMidi()
}

function sendGrooveToMidi() {
  const player = new mm.MIDIPlayer();
  player.requestMIDIAccess().then(() => {
    player.outputs = [player.availableOutputs[0]];
    player.start(modelSequence);
  })
}