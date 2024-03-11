API проект редактирования базы данных сотрудников построенный по архитектуре CRQS
- Для разделения команд и запросов (CRQS) используется MediatR
- Хранение кэша в Redis(Get запросов и в течение 30 минут сохраняет удаленный Id сотрудника)
- Логирование с помощью Serilog
- С использованием FluentValidation
- Модульные тесты

The API is an employee database editing project built on the CRQS architecture
- MediatR is used to separate commands and requests (CRQS)
- Cache storage in Redis (Get requests and saves the deleted employee Id for 30 minutes)
- Logging using Serilog
- Using FluentValidation
- Unit tests
