var HandLandmarker = {
    CreateHandLandmarker: function(screenWidth, screenHeight) {
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
            console.log(result);
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