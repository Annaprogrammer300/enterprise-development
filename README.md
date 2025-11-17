# Разработка корпоративных приложений
[Таблица с успеваемостью](https://docs.google.com/spreadsheets/d/1JD6aiOG6r7GrA79oJncjgUHWtfeW4g_YZ9ayNgxb_w0/edit?usp=sharing)

## Лабораторная работа 2: «Серверное приложение с Web API и аналитикой»

## Описание проекта
Проект представляет собой серверное приложение для управления данными поликлиники с RESTful Web API. Реализованы CRUD-операции для сущностей пациентов, врачей и записей на прием, а также аналитические запросы из первой лабораторной работы.

## Структура решения

### Проект Polyclinic
- **Сущности предметной области**: `Patient`, `Doctor`, `Appointment`
- **Перечисления**: `BloodGroup`, `Gender`, `RhesusFactor`, `Specializations`

### Проект Polyclinic.Tests
- `PolyclinicTests.cs` - unit-тесты функциональности (5 тестов)
- `TestDataSeeder.cs` - генератор тестовых данных (10 пациентов, 10 врачей, 11 записей)
- `TestFixture.cs` - для разделения состояние данных между тестами

### Проект Polyclinic.Application.Contracts
**Интерфейсы сервисов**:
  - `IApplicationService.cs` - общий интерфейс для CRUD операций
  - `IAnalyticsService.cs` - интерфейс аналитического сервиса
  - `IPolyclinicManager.cs` - интерфейс доменного менеджера

**DTO для пациентов**:
  - `PatientDto.cs` - DTO для чтения данных пациента
  - `PatientCreateUpdateDto.cs` - DTO для создания/обновления пациента
  - `IPatienService.cs` - интерфейс сервиса для пациентов

**DTO для врачей**:
  - `DoctorDto.cs` - DTO для чтения данных врача
  - `DoctorCreateUpdateDto.cs` - DTO для создания/обновления врача
  - `IDoctorService.cs` - интерфейс сервиса для врачей

**DTO для записей**:
  - `AppointmentDto.cs` - DTO для чтения данных записи
  - `AppointmentCreateUpdateDto.cs` - DTO для создания/обновления записи
  - `IAppointmentService.cs` - интерфейс сервиса записей

### Polyclinic.Application
**Сервисы приложения**: 
  - `PatientService` - CRUD операции для пациентов
  - `DoctorService` - CRUD операции для врачей
  - `AppointmentService` - CRUD операции для записей
  - `AnalyticsService` - аналитические запросы
- `PolyclinicProfile` (AutoMapper)

### Polyclinic.Infrastructure.InMemory 
**InMemory менеджеры**: 
  - `PatientInMemoryManager`
  - `DoctorInMemoryManager` 
  - `AppointmentInMemoryManager`

**Интерфейс**: `IManager<T, int>` - для доступа к данным

### Polyclinic.Api.Host (Web API слой)
**Контроллеры**: 
  - `PatientsController` - CRUD для пациентов
  - `DoctorsController` - CRUD для врачей  
  - `AppointmentsController` - CRUD для записей
  - `AnalyticsController` - аналитические endpoints

**Конфигурация**: `Program.cs`, `appsettings.json`

## Функциональность

### CRUD операции

#### Пациенты (`/api/patients`)
- `GET /api/patients` - получить всех пациентов
- `GET /api/patients/{id}` - получить пациента по ID
- `POST /api/patients` - создать нового пациента
- `PUT /api/patients/{id}` - обновить пациента
- `DELETE /api/patients/{id}` - удалить пациента

#### Врачи (`/api/doctors`)
- `GET /api/doctors` - получить всех врачей
- `GET /api/doctors/{id}` - получить врача по ID
- `POST /api/doctors` - создать нового врача
- `PUT /api/doctors/{id}` - обновить врача
- `DELETE /api/doctors/{id}` - удалить врача

#### Записи на прием (`/api/appointments`)
- `GET /api/appointments` - получить все записи
- `GET /api/appointments/{id}` - получить запись по ID
- `POST /api/appointments` - создать новую запись
- `PUT /api/appointments/{id}` - обновить запись
- `DELETE /api/appointments/{id}` - удалить запись

### Аналитические endpoints (`/api/analytics`)

1. **Врачи с опытом работы**
   - `GET /api/analytics/doctors/experience/{minExperience}`
   - Вывод ID врачей со стажем работы не менее указанного количества лет

2. **Пациенты по врачу**
   - `GET /api/analytics/patients/by-doctor/{doctorId}`
   - Вывод ФИО пациентов, записанных к указанному врачу (упорядочено по ФИО)

3. **Повторные приемы**
   - `GET /api/analytics/appointments/repeated-count?lastMonth={date}&today={date}`
   - Подсчет количества повторных приемов пациентов за указанный период

4. **Пациенты старше возраста с несколькими врачами**
   - `GET /api/analytics/patients/over-age-with-multiple-doctors?age={age}&date={date}`
   - Вывод дат рождения пациентов старше указанного возраста, записанных к нескольким врачам

5. **Приемы в кабинете за месяц**
   - `GET /api/analytics/appointments/in-cabinet/{roomNumber}?currentDate={date}`
   - Вывод информации о приемах за указанный месяц в выбранном кабинете

## Unit-тесты

### Реализованные тесты:

1. **DoctorsWithExperienceAtLeast10Years**  
   Вывод информации о всех врачах со стажем работы не менее 10 лет

2. **PatientsBySpecificDoctor**  
   Вывод информации о всех пациентах, записанных к указанному врачу (упорядочено по ФИО)

3. **CountOfRepeatedAppointmentsLastMonth**  
   Подсчет количества повторных приемов пациентов за последний месяц

4. **PatientsOver30WithMultipleDoctors**  
   Вывод информации о пациентах старше 30 лет, записанных к нескольким врачам (упорядочено по дате рождения)

5. **AppointmentsInSelectedCabinetCurrentMonth**  
   Вывод информации о приемах за текущий месяц в выбранном кабинете
