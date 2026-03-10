

# Requirements

ก่อนเริ่มใช้งาน ต้องติดตั้งเครื่องมือดังนี้

* **Node.js** >= 18
* **npm** หรือ **yarn** หรือ **pnpm**

ตรวจสอบเวอร์ชัน

```bash
node -v
npm -v
```

---

# Installation

Clone repository

```bash
git clone https://github.com/Tanachuns/test-07.git
```

เข้าไปที่ project directory

```bash
cd test-07/client
```

ติดตั้ง dependencies

```bash
npm install
```

หรือ

```bash
yarn install
```

หรือ

```bash
pnpm install
```

---

# Environment Variables

สร้างไฟล์

```
.env
```

ตัวอย่าง

```env
VITE_API_URL=http://localhost:5000
```

**หมายเหตุ**

Vite จะ expose เฉพาะ environment variables ที่ขึ้นต้นด้วย

```
VITE_
```

---

# Run Development Server

รัน development server

```bash
npm run dev
```

หรือ

```bash
yarn dev
```

หลังจากรันแล้วสามารถเปิดเว็บได้ที่

```
http://localhost:5173
```

---

