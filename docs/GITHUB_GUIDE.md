# Hướng dẫn đẩy repo lên GitHub cho đẹp

## 1. Tạo repo

Tên repo gợi ý:

```text
csharp-practice-200
```

Mô tả repo:

```text
200 C# exercises with offline judge and WinForms practice app.
```

## 2. Khởi tạo Git

```bat
git init
git add .
git commit -m "Initial C# practice repository with offline judge"
```

## 3. Commit theo tiến độ

Ví dụ:

```bat
git add solutions/P001 solutions/P002 solutions/P003
git commit -m "Solve P001-P003 basic math exercises"
```

Không nên commit một lần duy nhất 200 bài. Nhà tuyển dụng nhìn lịch sử commit đều sẽ tốt hơn.

## 4. Khi làm xong một cụm

Cập nhật README hoặc tạo issue/checklist.

Ví dụ:

```md
- [x] P001-P030 Basic input/output
- [x] P031-P075 Loops
- [ ] P076-P115 Arrays
```

## 5. Repo này nên ghi trong CV thế nào?

Không cần đưa repo bài tập lên phần dự án chính. Có thể để ở GitHub profile hoặc mục học tập.

Phần dự án chính vẫn nên là:

- WinForms CRUD + SQL Server
- ASP.NET Core Web API CRUD
- PHP/Laravel/MySQL project
