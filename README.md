<div align="center">
  <h1>Kruskal and Prim Algorithm Simulation</h1>
</div>

## Mục lục
1. [Giới thiệu](#giới-thiệu)
2. [Tính năng](#tính-năng)
3. [Cài đặt & Môi trường](#cài-đặt--môi-trường)
---

## Giới thiệu

![prim_simulation](https://github.com/user-attachments/assets/d001b5cd-192d-4bad-9305-b5e20a9f4467)

**Kruskal and Prim Algorithm Simulation** là dự án mô phỏng trực quan hai thuật toán cây khung nhỏ nhất (Minimum Spanning Tree – MST):

- Thuật toán **Prim**
- Thuật toán **Kruskal**

Dự án kết hợp:
- **C# WinForms**: nhập và kiểm tra dữ liệu đồ thị
- **Python + NetworkX + Matplotlib**: mô phỏng thuật toán theo từng bước

Mục tiêu chính của dự án là **phục vụ học tập**, giúp người học hiểu rõ:
- Cách hoạt động của từng thuật toán
- Sự khác nhau giữa Prim và Kruskal
- Trình tự xét cạnh, xây dựng MST và tổng trọng số

---

## Tính năng

### Nhập đồ thị
- Nhập bằng **Danh sách cạnh**
- Nhập bằng **Ma trận kề**
- Nhập thủ công hoặc sinh ngẫu nhiên
- Hỗ trợ sinh đồ thị **liên thông**

### Mô phỏng trực quan
- Hiển thị từng bước thuật toán
- Tô màu:
  - Đỉnh đã thăm
  - Cạnh đang xét
  - Cạnh thuộc MST
- Hiển thị **giả mã (pseudocode)** được highlight theo bước
- Log mô tả chi tiết từng thao tác

### Tùy chỉnh vị trí đỉnh
- Kéo – thả các đỉnh bằng chuột
- Lưu lại vị trí để tái sử dụng

---

## Cài đặt & Môi trường

### Yêu cầu
- **Windows**
- **Python 3.9 trở lên**
- .NET (để chạy WinForms)

### Cài thư viện Python
Trong thư mục gốc của dự án, chạy:

```bash
pip install -r requirements.txt
```
### Định dạng dữ liệu
graph.json

```bash
{
  "nodes": [0, 1, 2, 3],
  "edges": [
    { "u": 0, "v": 1, "w": 2 },
    { "u": 1, "v": 2, "w": 3 }
  ]
}
```

positions.json

```bash
{
  "0": [0.1, 0.5],
  "1": [0.4, 0.8],
  "2": [0.7, 0.3]
}
```
