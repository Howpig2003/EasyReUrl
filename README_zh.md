# EasyReUrl - 簡易短網址服務

此 demo 為作者用於學習與探索 ASP.NET MVC 架構的練習作品。

## 功能

- 生成 6 位短網址（例如 `https://yourdomain.com/xyzabc`），使用 58 個字符集（A-Z、a-z、2-9，排除 0、1、l、O、I 以提高可讀性）。
- 將短網址重定向至原始網址。
- 重用創建超過 7 天的短網址以處理碰撞。
- 使用 SQLite 資料庫儲存網址資料。
- 提供友善的網頁介面，支援錯誤處理。

## 技術棧

- ASP.NET Core (.NET 8.0)  
- SQLite 與 Entity Framework Core (v9.0.7)  
- Razor 視圖（`Index.cshtml`）與自訂 CSS（`index.css`）  
- MVC 架構

## AI 使用  

本專案在 xAI 開發的 Grok 協助下完成，Grok 參與程式碼生成（例如碰撞處理邏輯）、除錯（例如解決 `SqliteException`）以及文件撰寫。

## 開源許可

採用 MIT 許可證。
