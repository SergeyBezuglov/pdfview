# ����� ����������

## ���������� ������

T1 - ��� �� ��������� <br>
T2 - �������� <br>
T3 - ��������� ���������
```csharp
public void T1_T2_T3()...
public void HandleRegisterCommandForms_WhenUserIsValid_ShouldCreateAndReturnAuthenticationResult()
```
## ��������� ����������� � AD (����������� � ���� AuthenticationModules appsettings.json)
```json

  "AD": {
      "Type": "ActiveDirectory",
      "Name": "Active Directory",
      "Settings": {
        "Host": "devHost",
        "Domain": "dev.net",
        "AccessDomainUserName": "dev\\�������������",
        "AccessDomainUserPassword": "������_123",
        "AllowNestedDomain": "true"
      }
    }
```
## ������ � ���������� ������
```
<major>.<minor>.<patch>
```
