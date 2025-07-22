📘 Learnava – Scalable Educational Platform
Learnava is a full-stack course management system designed for delivering dynamic, media-rich educational content.
Built using ASP.NET Core, it supports student enrollment, instructor control panels, role-based access, video integration, and polished UI/UX across devices.

⚙️ Tech Stack
| Layer         | Tools & Libraries                                                    |
|---------------|----------------------------------------------------------------------|
| Backend       | ASP.NET Core MVC, C#, Entity Framework Core                          |
| Frontend      | Razor Pages, Bootstrap 5, DataTables, SweetAlert2, Toastr            |
| Database      | SQL Server                                                           |
| Deployment    | Docker (ready), IIS-hosted                                           |
| Auth & Roles  | Clerk (Auth), [Authorize] attributes                                 |
| Multimedia    | YouTube iframe embedding, course thumbnails                          |
| File Storage  | Static images uploaded to wwwroot/Images/...                         |



🔐 Role-Based Access
Admin, Instructor, and Student roles with dynamic views and permissions

Custom dashboards and restricted actions per role

🎓 Course Builder
Add/update courses with multimedia, structured descriptions, and instructor linking

Embedded YouTube videos, thumbnails with image upload, and layout-aware video display

📊 Dashboard & Reporting
AJAX-powered DataTables: sortable by instructor, date, student

SweetAlert2 confirmations and Toastr feedback for smooth admin interactions

🎥 Seamless Video Layout
Left-side video list + right-side embedded video

Navigation without page reloads for fluid learning

🧾 Enrollment Workflow
One-click enroll using POST forms

Student redirection to personalized course content

🖼️ Static File Management
Image uploads saved to organized folders

Cross-platform path normalization + fallback support

📁 Folder Structure
Learnava/
├── Controllers/
├── Services/
├── Models/
├── Views/
│   └── Courses/
│   └── Shared/
├── wwwroot/
│   └── Images/
│       └── Courses/
│   └── js/
│   └── css/


🧠 Developer Highlights
Modular service-layer architecture with repository patterns

Bulletproof deployment logic: static file access, path mapping, and no double-wrapping of wwwroot

SQL queries optimized for user-facing strings and formatted reporting

Clean UI components built with reusable Bootstrap cards

