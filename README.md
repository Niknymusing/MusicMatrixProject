# MusicMatrixHackathon
Code for the Music Matrix project.

## Open Sound Control Web Bridge

Creates a simple bridge between your Web page and an OSC app or device.

    .----------.              .----------------------.    .------------------.                 .----------.
    | OSC  app | --tcp/udp--> | bridge.js OSC server | => | socket.io client | --websockets--> | web page |
    `--(3334)--'              `-------( 3333 )-------'    `------------------'                 `----------'
         ^                                                                                          |
         |                                                                                          |
         |                                                                                          |
         |                .----------------------.    .------------------.                          |
         `---tcp/udp----- | bridge.js OSC client | <= | socket.io server | <-------websockets-------'
                          `----------------------'    `-----( 8081 )-----'

## Pose

Pose takes a 2D vector illustration and animates its containing curves in real-time based on the recognition result from PoseNet. It borrows the idea of skeleton-based animation from computer graphics and applies it to vector characters.

## Using OSC together with pose

Run the bridge app on your machine (localhost):

```
$ cd MusicMatrixHackathon
$ node bridge.js
```

Build and run Pose:

```
$ cd pose
$ yarn
$ yarn watch
```