# ���������� �� ������ � ��
## �������� �������� ��
������ ���������� �������� �� ��� ��
```
add-migration InitialCreate -Project PIMS.Migrations.MSQL -StartupProject PIMS.Web -Args "--ProviderName MSSQLServer"
add-migration InitialCreate -Project PIMS.Migrations.PostgreSQL -StartupProject PIMS.Web -Args "--ProviderName PostgreSQLServer"
```

## ���������� ��
������ ��������� ��� ��
```
update-database -Project PIMS.Migrations.MSQL -StartupProject PIMS.Web -Args "--ProviderName MSSQLServer"
update-database -Project PIMS.Migrations.PostgreSQL -StartupProject PIMS.Web -Args "--ProviderName PostgreSQLServer"
```
