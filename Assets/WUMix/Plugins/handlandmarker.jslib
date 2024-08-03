var HandLandmarker = {
    CreateHandLandmarker: function(screenWidth, screenHeight, callback) {
        const webcamElement = document.getElementById("webcam");
        webcamElement.width = screenWidth;
        webcamElement.height = screenHeight;

        const hands = new Hands({locateFile: (file) => {
            return `https://cdn.jsdelivr.net/npm/@mediapipe/hands/${file}`;
        }});
        hands.setOptions({
            maxNumHands: 2,
            modelComplexity: 1,
            minDetectionConfidence: 0.5,
            minTrackingConfidence: 0.5
        });

        hands.onResults((result) => {
            if (result.multiHandLandmarks) {
                var landmarks = JSON.stringify(result.multiHandLandmarks);
                if (landmarks == undefined) return;

                var landmarksBufferSize = lengthBytesUTF8(landmarks) + 1;
                var landmarksBuffer = _malloc(landmarksBufferSize);
                stringToUTF8(landmarks, landmarksBuffer, landmarksBufferSize);
                var image =  result.image;
                Module['dynCall_viii'](callback, landmarksBuffer, image.width, image.height);
                _free(landmarksBuffer);
            }
        });

        const camera = new Camera(webcamElement, {
            onFrame: async () => {
                await hands.send({image: webcamElement});
            },
            width: screenWidth,
            height: screenHeight
        });
        camera.start();
    }
};

mergeInto(LibraryManager.library, HandLandmarker); 