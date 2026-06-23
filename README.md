# C# Practice 200

Repo luyện C# gồm 200 bài tập từ cơ bản đến nâng cao, có giao diện WinForms để viết code và bộ chấm tự động chạy offline.

## Mục tiêu

- Luyện chắc nền tảng C# qua từng bài nhỏ.
- Tự viết lời giải trong thư mục `solutions`.
- Chấm đúng/sai bằng test case có sẵn.
- Theo dõi quá trình học qua commit GitHub.

## Tính năng

- Danh sách 200 bài tập.
- Tìm kiếm và lọc bài theo mức độ.
- Giao diện đọc đề, viết code và chấm bài.
- Chạy test mẫu hoặc toàn bộ test.
- Bộ chấm console dùng được ngoài giao diện.

## Cấu trúc thư mục

```text
csharp-practice-200/
├─ exercises/
│  └─ problems.json              # đề bài + test case
├─ solutions/                    # code lời giải của bạn
├─ src/
│  ├─ CSharpPractice.App/        # giao diện WinForms
│  └─ CSharpPractice.Judge/      # bộ chấm tự động
├─ README.md
├─ INDEX.md                      # danh sách 200 bài
├─ run.bat                       # mở giao diện
├─ judge.bat                     # chấm bài bằng console
├─ list.bat                      # xem danh sách bài
└─ .gitignore
```

## Yêu cầu

Cài .NET SDK 8 trên Windows.

Kiểm tra bằng lệnh:

```bat
dotnet --version
```

## Chạy giao diện

Bấm đúp:

```bat
run.bat
```

Hoặc chạy bằng terminal:

```bat
dotnet run --project src/CSharpPractice.App
```

## Chấm bài bằng console

Xem danh sách bài:

```bat
list.bat
```

Chấm bài P001:

```bat
judge.bat P001
```

Chạy test mẫu:

```bat
judge.bat P001 --sample
```

Chấm toàn bộ bài đã làm:

```bat
judge.bat --all
```

## Cách làm bài

Mở giao diện, chọn bài, viết code rồi bấm **Chấm bài**. Khi lưu, code sẽ nằm trong:

```text
solutions/P001/Program.cs
solutions/P002/Program.cs
...
```

Sau khi bài pass, commit code lên GitHub.

Ví dụ:

```bash
git add solutions/P001/Program.cs
git commit -m "Solve P001 sum two numbers"
git push
```

## Chủ đề trong 200 bài

- Nhập xuất, biến, kiểu dữ liệu
- Điều kiện `if`, `switch`
- Vòng lặp `for`, `while`
- Mảng, `List<T>`
- Chuỗi
- Hàm, `return`, `void`
- Đệ quy cơ bản
- Tìm kiếm, sắp xếp
- Stack, Queue
- Quy hoạch động cơ bản
- OOP: class, object, constructor, method
- Xử lý dữ liệu dạng CSV/key-value/log

## Ghi chú

Repo này là nơi lưu quá trình luyện C#