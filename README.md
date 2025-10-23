# Разработка корпоративных приложений
[Таблица с успеваемостью](https://docs.google.com/spreadsheets/d/1JD6aiOG6r7GrA79oJncjgUHWtfeW4g_YZ9ayNgxb_w0/edit?usp=sharing)

## Лабораторная работа 1: «Классы» - Реализация объектной модели данных и unit-тестов

## Описание проекта
Проект представляет собой систему управления данными поликлиники, включающую информацию о пациентах, врачах и записях на прием. Реализована структура классов предметной области и unit-тесты.

## Структура решения

### Проект Polyclinic
**Компоненты:**

#### Перечисления (Enum)
- `BloodGroup.cs` - группы крови пациента (I, II, III, IV)
- `Gender.cs` - пол пациента (Male, Female)
- `RhesusFactor.cs` - резус-фактор пациента (Positive, Negative)
- `Specializations.cs` - специализации врачей

#### Классы предметной области
- `Patient.cs` - пациент поликлиники
- `Doctor.cs` - врач поликлиники  
- `Appointment.cs` - запись на прием

### Проект Polyclinic.Tests
- `PolyclinicTests.cs` - unit-тесты функциональности (5 тестов)
- `TestDataSeeder.cs` - генератор тестовых данных (10 пациентов, 10 врачей, 11 записей)
- `TestFixture.cs` - для разделения состояние данных между тестами

## Модели данных

### Пациент (Patient)
- **PassportNumber** - номер паспорта
- **FullName** - ФИО пациента
- **Gender** - пол (перечисление Gender)
- **BirthDate** - дата рождения
- **Address** - адрес проживания
- **BloodGroup** - группа крови (перечисление BloodGroup)
- **RhesusFactor** - резус-фактор (перечисление RhesusFactor)
- **Phone** - контактный телефон

### Врач (Doctor)
- **PassportNumber** - номер паспорта
- **FullName** - ФИО врача
- **BirthYear** - год рождения
- **Specialization** - специализация (перечисление Specializations)
- **Experience** - стаж работы (в годах)

### Запись на прием (Appointment)
- **Patient** - пациент 
- **Doctor** - врач 
- **DateTime** - дата и время приема
- **RoomNumber** - номер кабинета
- **IsRepeat** - повторный прием (true/false)

## Unit-тесты

### Реализованные тесты:

1. **Doctors_With_Experience_AtLeast_10Years**  
   Вывод информации о всех врачах со стажем работы не менее 10 лет

2. **Patients_By_Specific_Doctor**  
   Вывод информации о всех пациентах, записанных к указанному врачу (упорядочено по ФИО)

3. **Count_Of_Repeated_Appointments_LastMonth**  
   Подсчет количества повторных приемов пациентов за последний месяц

4. **Patients_Over30_With_Multiple_Doctors**  
   Вывод информации о пациентах старше 30 лет, записанных к нескольким врачам (упорядочено по дате рождения)

5. **Appointments_In_Selected_Cabinet_CurrentMonth**  
   Вывод информации о приемах за текущий месяц в выбранном кабинете
