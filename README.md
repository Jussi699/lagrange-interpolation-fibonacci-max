# Lagrange Interpolation & Fibonacci Maximization (GUI)

## 📌 Overview

This project is a C# GUI application designed to numerically find the maximum of a function defined as:

**f(x) - g(x) → max**

The functions **f(x)** and **g(x)** are not given analytically, but as discrete sets of points. To evaluate intermediate values, the program uses **Lagrange polynomial interpolation**, allowing smooth approximation over a continuous range.

The maximum is then found using the **Fibonacci search method**, which efficiently narrows the search interval and minimizes the number of function evaluations.

---

## 🚀 Features

* Graphical User Interface (GUI)
* Input of function values as discrete data points
* Lagrange interpolation for function approximation
* Fibonacci method for numerical maximization
* Modular and extensible code structure

---

## 🛠️ Technologies

* C# (.NET)
* WinForms

---

## ▶️ Requirements & Dependencies

To successfully build and run the project, make sure the following dependencies are available:

* **.NET SDK / Runtime**
* `Aspose.PDF.dll`
* `Microsoft.Web.WebView2.Core`
* `Microsoft.NET.ILLink.Tasks`

📌 You can install missing packages via NuGet or by adding required DLLs manually.

---

## ▶️ How to Run

1. Clone the repository:

   ```bash
   git clone https://github.com/Jussi699/lagrange-interpolation-fibonacci-max.git
   ```

2. Open the project in Visual Studio

3. Restore dependencies (if needed):

   ```bash
   dotnet restore
   ```

4. Build and run the project:

   ```bash
   dotnet run
   or
   F5
   ```

---

## 📊 Input Data Format (Example)

Functions are defined using XML files with a list of points.

### Example:

```xml
<Points>
  <Point>
    <X>0</X>
    <Y>0</Y>
  </Point>
  <Point>
    <X>2</X>
    <Y>8</Y>
  </Point>
  <Point>
    <X>6</X>
    <Y>8</Y>
  </Point>
...
</Points>
```

📌 Each `<Point>` represents a coordinate (**X**, **Y**) of the function.  
📌 At least two points are required for interpolation.  
📌 More points → better approximation accuracy.  
 
---

## 📊 Description of Methods

### 🔹 Lagrange Interpolation

Used to reconstruct functions **f(x)** and **g(x)** from given discrete points.

### 🔹 Fibonacci Search Method

Used to efficiently find the maximum of the function **f(x) - g(x)** within a specified interval.

---

## 📎 Notes

* Ensure all required DLLs are properly referenced
* The accuracy of results depends on the number and distribution of input points
