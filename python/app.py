# ✅ Local FastAPI YOLOv8 Detection Server

import io
import numpy as np
from PIL import Image
from fastapi import FastAPI, UploadFile, File
from fastapi.responses import JSONResponse
from ultralytics import YOLO
import uvicorn

# Load pretrained YOLOv8n model (COCO has cat/dog)
model = YOLO("yolov8n.pt")

# Create FastAPI app
app = FastAPI(title="CatDog Detector API", description="Detect cats and dogs using YOLOv8")

@app.post("/detect")
async def detect_image(file: UploadFile = File(...)):
    """Receives an image and returns cat/dog detections."""
    img_bytes = await file.read()
    img = Image.open(io.BytesIO(img_bytes)).convert("RGB")
    img_array = np.array(img)

    results = model(img_array)
    detections = []

    for box in results[0].boxes:
        cls_id = int(box.cls[0])
        label = model.names[cls_id]
        if label not in ["cat", "dog"]:
            continue
        x1, y1, x2, y2 = map(int, box.xyxy[0])
        detections.append({
            "label": label,
            "confidence": float(box.conf[0]),
            "x": x1, "y": y1,
            "w": x2 - x1, "h": y2 - y1
        })

    return JSONResponse({"detections": detections})

if __name__ == "__main__":
    # Run locally on http://127.0.0.1:8000
    uvicorn.run(app, host="0.0.0.0", port=8000)
