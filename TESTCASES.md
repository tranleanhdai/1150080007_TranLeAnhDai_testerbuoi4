# Test cases (>= 15) – Organization Form

> Bạn có thể copy phần này vào báo cáo.

| TC | Mục tiêu | Input | Kỳ vọng |
|---:|---|---|---|
| 01 | Save hợp lệ | OrgName=Org A, Address=HCM, Phone=0987654321, Email=a@b.com | Lưu DB, hiện "Save successfully", enable Director |
| 02 | OrgName rỗng | OrgName="   " | Không lưu, báo lỗi OrgName |
| 03 | OrgName < 3 | OrgName="AB" | Không lưu, báo lỗi OrgName |
| 04 | OrgName = 3 | OrgName="ABC" | Lưu thành công |
| 05 | OrgName = 255 | OrgName=255 ký tự | Lưu thành công |
| 06 | OrgName > 255 | OrgName=256 ký tự | Không lưu, báo lỗi OrgName |
| 07 | Trùng tên (khác hoa/thường) | Save "ABC Company" rồi save "abc company" | Không lưu, báo "Organization Name already exists" |
| 08 | Trùng tên (có khoảng trắng) | Save "ABC Company" rồi save "   abc company   " | Không lưu, báo trùng |
| 09 | Phone bỏ trống | Phone="  " | Lưu thành công |
| 10 | Phone chứa chữ | Phone="0987ABCD" | Không lưu, báo lỗi Phone |
| 11 | Phone < 9 | Phone=8 số | Không lưu, báo lỗi Phone |
| 12 | Phone = 9 | Phone=9 số | Lưu thành công |
| 13 | Phone = 12 | Phone=12 số | Lưu thành công |
| 14 | Phone > 12 | Phone=13 số | Không lưu, báo lỗi Phone |
| 15 | Email bỏ trống | Email="  " | Lưu thành công |
| 16 | Email sai format | Email="not-an-email" | Không lưu, báo lỗi Email |
| 17 | Email đúng | Email="a@b.com" | Lưu thành công |
| 18 | Director bị khóa trước Save | Mở form, bấm Director | Không bấm được (disabled) |
| 19 | Director mở sau Save | Save OK rồi bấm Director | Mở Director Management với đúng OrgName/Id |
