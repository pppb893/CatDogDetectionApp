# 🐾 Cat & Dog Detection App

A cross-language **AI detection system** built with **C# (WinForms)** and **Python (FastAPI + YOLOv8)**.  
This project demonstrates how a Windows GUI can interact with an AI backend to detect and count cats and dogs in images in real time.

---

## 📘 Overview

This project was developed to demonstrate the integration of a **Windows application (C#)** with an **AI model served by Python FastAPI**.  
Users can upload an image, which will be sent to the Python backend for object detection using **YOLOv8**.  
The detection results (label, confidence, and bounding boxes) are then displayed directly on the GUI.

### Main Capabilities
- Detects and counts cats 🐱 and dogs 🐶
- Displays bounding boxes with different colors (Green = Cat, Blue = Dog)
- Lets users switch between "Cat", "Dog", and "Both" modes
- Displays messages if no object is detected
- Updates the app title bar with total object counts

---

## 🧠 Tech Stack

| Component | Description |
|------------|--------------|
| **Languages** | Python 3.10+, C# (.NET Framework / .NET 6+) |
| **AI Model** | Ultralytics YOLOv8n (Pretrained on COCO dataset) |
| **Backend Framework** | FastAPI + Uvicorn |
| **Frontend GUI** | Windows Forms (C# + EmguCV) |
| **Libraries** | OpenCV, Pillow, Numpy, HttpClient |
| **Communication** | REST API between FastAPI and C# client |

---

## ⚙️ Installation & Setup

### 🐍 Python Backend (FastAPI + YOLOv8)

1. **Create and activate a virtual environment**
   ```bash
   python -m venv venv
   venv\Scripts\activate    # Windows
   # or
   source venv/bin/activate # macOS / Linux

Install dependencies
pip install fastapi uvicorn ultralytics pillow numpy opencv-python


Run the FastAPI server
python app.py


Once started, it will show:
Uvicorn running on http://127.0.0.1:8000


C# Frontend (WinForms App)
Open the solution CatDogDetectionApp.sln in Visual Studio
Open the file Form1.cs and set:

private string apiUrl = "http://127.0.0.1:8000/detect";

Run the application
When prompted, upload an image to start detection

🧩 Demonstration
When an image containing cats or dogs is uploaded:
✅ Green boxes = Cats
✅ Blue boxes = Dogs
🧮 The title bar updates as:
Cat & Dog Detector | Cats: 3  Dogs: 2

⚠️ If no object of the selected type is found, a popup appears:
“No cats detected!”
“No dogs detected!”
“No cats or dogs detected!”

💡 Features
Real-time object detection
Separate display modes for Cat / Dog / Both
Visual distinction using color-coded boxes
Automatic object counting
Clear message alerts when no object is detected

🧩 Design Philosophy
This project was designed to:
Be simple and educational for beginners in AI and Computer Vision
Clearly separate Frontend (C#) and Backend (Python) responsibilities
Use industry-standard tools such as FastAPI, YOLOv8, and OpenCV
Serve as a foundation for future projects such as live video detection or IoT applications

you are free to use, modify, and share it with attribution.
