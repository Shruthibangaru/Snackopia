from flask import Flask, render_template, Response
import cv2
from roboflow import Roboflow


app = Flask(__name__)

api_key = "zXsBRfrEr4op8gSkhB7C"
project_name = "minor2.0"
workspace_name = "groceries-dwrer"

rf = Roboflow(api_key=api_key)
project = rf.workspace(workspace_name).project(project_name)
model = project.version(1).model


cap = cv2.VideoCapture(0)

def generate_frames():
    while True:
        ret, frame = cap.read()
        if not ret:
            break
        
    
        predictions = model.predict(frame, confidence=40, overlap=30).json()
        
     
        for prediction in predictions['predictions']:
            x1, y1 = int(prediction['x'] - prediction['width'] / 2), int(prediction['y'] - prediction['height'] / 2)
            x2, y2 = int(prediction['x'] + prediction['width'] / 2), int(prediction['y'] + prediction['height'] / 2)
            label = prediction['class']
            confidence = prediction['confidence']
        
            cv2.rectangle(frame, (x1, y1), (x2, y2), (0, 255, 0), 2)
            cv2.putText(frame, f"{label} {confidence:.2f}", (x1, y1 - 10), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0, 255, 0), 2)

        ret, buffer = cv2.imencode('.jpg', frame)
        frame = buffer.tobytes()

  
        yield (b'--frame\r\n'
               b'Content-Type: image/jpeg\r\n\r\n' + frame + b'\r\n')

@app.route('/')
def index():
    return render_template('index.html')

@app.route('/video_feed')
def video_feed():
    return Response(generate_frames(), mimetype='multipart/x-mixed-replace; boundary=frame')

if __name__ == "__main__":
    app.run(debug=True)