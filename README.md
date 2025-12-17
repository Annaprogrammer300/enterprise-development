# Разработка корпоративных приложений
[Таблица с успеваемостью](https://docs.google.com/spreadsheets/d/1JD6aiOG6r7GrA79oJncjgUHWtfeW4g_YZ9ayNgxb_w0/edit?usp=sharing)

## Лабораторная работа 4: «Инфраструктура» - gRPC и генерация данных

## Описание проекта
Четвёртая лабораторная работа реализует **инфраструктуру для генерации и передачи контрактов между сервисами**. Основная задача — создать отдельное приложение-генератор, которое передает данные серверу через gRPC в потоковом режиме, с поддержкой PostgreSQL и интеграцией с Aspire.

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
  - `GeneratorController` - управление генерацией 
### HostedServices/
- `PatientGeneratorHostedService` - фоновая генерация 
- `PatientConsumerHostedService` - обработка сообщений 

**Конфигурация**: `Program.cs`, `appsettings.json`

### Polyclinic.Infrastructure.EfCore

**Назначение**: содержит реализацию доступа к данным через Entity Framework Core и PostgreSQL.

- `PolyclinicDbContext` - DbContext для работы с БД PostgreSQL
- `DbSeeder` - Данные для работы миграции
- `PolyclinicDbContextFactory` - Для создания миграций через Add-Migration

### Data/Migrations/
- Миграция создания схемы и заполнения БД

### Repositories/
- `PatientEfCoreManager` - Репозиторий для работы с пациентами
- `DoctorEfCoreManager` - Репозиторий для работы с врачами
- `AppointmentEfCoreManager` - Репозиторий для работы с записями

## Проект Polyclinic.ServiceDefaults
**Назначение**: содержит стандартную конфигурацию для сервисов Aspire.

## Проект Polyclinic.AppHost
**Назначение**: оркестратор Aspire для управления сервисами и их развёртыванием.

### Program.cs
Конфигурация оркестратора Aspire:
- Создание DistributedApplicationBuilder
- Добавление ресурса PostgreSQL
- Добавление сервиса API
- Запуск оркестратора

### appsettings.json
Конфигурация и параметры запуска Aspire

## Polyclinic.Generator 
**Назначение**: Самостоятельное приложение для генерации и отправки пациентов
### Generator/
- `PatientGenerator.cs` - Генератор случайных пациентов
### Services/
- `IProducerService.cs` - Интерфейс сервиса отправки (провайдер контрактов)
- `ProducerService.cs` - Реализация отправки данных в БД

## Polyclinic.Generator.Grpc.Host (gRPC хост для потоковой передачи)
**Назначение**: Сервис с gRPC API для потоковой передачи пациентов
- `Program.cs` - Конфигурация приложения генератора
### Services/
- `PatientGrpcGeneratorService.cs` -  реализация gRPC

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

### Генератор пациентов (`/api/generator/generate`)
- **GET /api/generator/generate** - запуск генерации пациентов
  - Параметры:
    - `batchSize` (int, default=10) - размер батча
    - `payloadLimit` (int, default=5) - количество батчей
    - `waitTime` (int, default=2) - пауза между батчами в сек
  - Возвращает: список сгенерированных пациентов (PatientDto)

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
