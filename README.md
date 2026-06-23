# CSharp Practice 200 - Offline Judge + WinForms GUI

Bộ 200 bài tập C# từ cơ bản đến khá/nâng cao, có **đề bài**, **test case**, **bộ chấm tự động** và **giao diện WinForms** để luyện giống các nền tảng code online nhưng chạy offline trên máy Windows.

Mục tiêu repo này:

- Luyện nền tảng C# một cách có hệ thống.
- Có lịch sử commit rõ ràng trên GitHub.
- Có thể cho nhà tuyển dụng thấy quá trình học, cách tổ chức code, khả năng tự luyện và hoàn thành bài.

> Repo này là repo luyện tập
---

## Cấu trúc thư mục

```text
CSharpPractice200/
├─ exercises/
│  ├─ Problems/              # 200 đề bài P001 -> P200
│  └─ problems.json          # test case cho bộ chấm
├─ solutions/                # nơi bạn code lời giải từng bài
│  ├─ P001/Program.cs
│  ├─ P002/Program.cs
│  └─ ...
├─ src/
│  ├─ CSharpPractice.Judge/  # bộ chấm console
│  └─ CSharpPractice.App/    # giao diện WinForms
├─ docs/
│  ├─ ROADMAP.md
│  ├─ GITHUB_GUIDE.md
│  └─ PORTFOLIO_NOTE.md
├─ scripts/
│  ├─ run-app.bat
│  ├─ run-judge.bat
│  └─ list.bat
├─ run.bat                   # mở giao diện
├─ judge.bat                 # chấm bài bằng console
├─ list.bat                  # xem danh sách bài
├─ INDEX.md                  # danh sách 200 bài
└─ .gitignore
```

---

## Yêu cầu

Cài **.NET SDK 8** trên Windows.

Kiểm tra bằng lệnh:

```bat
dotnet --version
```

---

## Cách mở giao diện

Bấm đúp:

```text
run.bat
```

Hoặc chạy:

```bat
dotnet run --project src/CSharpPractice.App
```

Trong giao diện bạn có thể:

- Chọn bài bên trái.
- Đọc đề bài.
- Viết code C# trong khung code.
- Bấm **Lưu code**.
- Bấm **Chạy ví dụ**.
- Bấm **Chấm bài**.
- Bấm **Format code** nếu code bị dính dòng.
- Theo dõi tiến độ trong `.practice/progress.json`.

---

## Cách chấm bằng console

Xem danh sách bài:

```bat
list.bat
```

Chấm bài P001:

```bat
judge.bat P001
```

Chỉ chạy test ví dụ:

```bat
judge.bat P001 --sample
```

Chấm tất cả bài đã làm:

```bat
judge.bat --all
```

---

## Cách học đề xuất

Không cần làm một ngày quá nhiều rồi bỏ. Nên làm đều:

```text
Ngày 1-3:    P001 - P030   nhập xuất, toán, điều kiện
Ngày 4-7:    P031 - P075   vòng lặp, số học
Ngày 8-13:   P076 - P115   mảng, List, ma trận
Ngày 14-17:  P116 - P145   chuỗi
Ngày 18-22:  P146 - P165   hàm, đệ quy, thuật toán
Ngày 23-30:  P166 - P200   OOP, data, mini task
```

---

## Chủ đề có trong 200 bài

- Nhập xuất, biến, kiểu dữ liệu
- Toán học cơ bản
- Điều kiện `if`, `switch`
- Vòng lặp `for`, `while`, vòng lặp lồng nhau
- Mảng, `List<T>`
- Ma trận
- Chuỗi
- Hàm, `return`, `void`
- Đệ quy
- Tìm kiếm, sắp xếp
- Stack, Queue
- Quy hoạch động cơ bản
- OOP: class, object, constructor, method
- Xử lý dữ liệu dạng CSV/key-value/log
- Bài tổng hợp kiểu mini judge
