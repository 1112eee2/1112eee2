# Minecraft UI Window (Unity)

สคริปต์นี้สร้างหน้าต่าง UI สไตล์ Minecraft แบบเบื้องต้นด้วย Unity UI (UGUI) โดยใช้โค้ดสร้าง Canvas, หน้าต่างหลัก, ช่อง Inventory และ Hotbar อัตโนมัติเมื่อรันฉาก

## วิธีใช้งาน
1. สร้าง GameObject เปล่าในฉาก แล้วตั้งชื่อ (เช่น `MinecraftUiRoot`).
2. เพิ่มสคริปต์ `MinecraftUiWindow` ให้กับ GameObject นั้น.
3. กด Play เพื่อดูหน้าต่าง UI.

## ปรับแต่ง
- ปรับขนาดหน้าต่าง สี หรือจำนวนช่องได้จาก Inspector
- เปลี่ยนข้อความ Title หรือ Hint ได้ในโค้ด `MinecraftUiWindow`
