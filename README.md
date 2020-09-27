# SimpleMassMailing

This tools can send mass mailing, using simple **config.ini**, **data.txt** and **message.html** files.

## Usage

1. Creates 3 files in a folder: `config.ini`, `data.txt` and `message.html` with content describes below.
2. [Download from the Release page](https://github.com/dvoituron/SimpleMassMailing/releases) and copy the executable `SimpleMassMailing.exe` in the same folder.
3. Run this executable.

```
C:\MyEmailer>SimpleMassMailing.exe
1/2. Send to Denis@voituron.net (Y/N)? y         ... Sent
2/2. Send to Remy@voituron.net (Y/N)? y          ... Sent
Completed
```

## Config.ini

The config file is a "key=value" file where keys are:

- **Smtp**: IP or URL of the mail server (ex. mail.gandi.net).
- **Port**: Port of the mail server (ex. 587).
- **Ssl**: Use the mail server in a secure mode (ex. True).
- **From**: EMail of the sender (ex. noreply@blazorday.online).
- **FromDisplayName**: Displayed Name of the sender (ex. BlazorDay event).
- **Cc**: EMail of one CC target (only one email).
- **Login**: Authentication login to send email using the SMTP server.
- **Password**: Authentication password to send email using the SMTP server.
- **ContentFile**: HTML Content to send (ex. Message.html). See below.
- **Attachment**: Optional file to attach into the mail (ex. ./myevents.ics).
- **DataFile**: List of target emails and properties (ex. data.txt). See below.
- **PromptBeforeSend**: False to send all email in one shot. True to ask for each mail.

> All configurations can be overridden by executable arguments
> Ex: `SimpleMassMailing -login=my_sample_login -password=my_sample_password`

Sample
```
Smtp=mail.gandi.net
Port=587
Ssl=True
From=noreply@blazorday.online
FromDisplayName=BlazorDay event
Cc=
Login=<login>
Password=<password>
ContentFile=Message.html
Attachment=./myevents.ics
DataFile=data.txt
PromptBeforeSend=False
```

## Data.txt

The Data file contains all emails and all associated properties

- The first text must be the **email target**.
- All properties are separated with comas (;) and are identified using key=value.

  > Prefix a line with the symbol # to mark this line as a **comment line**.

Sample
```
# Emails                  ; LastName                 ; FirstName
Denis@voituron.net        ; LastName=Voituron        ; FirstName=Denis
Remy@voituron.net         ; LastName=Voituron        ; FirstName=Rémy
```

## Message.html

The Message file contains an html file used as body of the mail.
All properties included in Data.txt are remplaced when the symbol `{Key}` is occured.

Sample
```
<div>
  <p>Hello {FirstName} {LastName},</p>
  <p>
     <b>BlazorDay</b> is almost here. Starting tomorrow.<br />     
     Don’t miss the keynote with Daniel Roth, the Blazor Program Manager at Microsoft.
  </p>
  ...
</div>
```