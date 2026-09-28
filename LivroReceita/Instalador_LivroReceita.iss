[Setup]
AppName=LivroReceita
AppVersion=1.0
AppPublisher=SPSKILLS
; CRÍTICO: Instala fora de Program Files para o SQL Server conseguir anexar o .mdf sem erro de UAC
DefaultDirName=C:\LivroReceita
DefaultGroupName=LivroReceita
OutputBaseFilename=Instalador_LivroReceita
Compression=lzma2/ultra
SolidCompression=yes
PrivilegesRequired=admin

[Tasks]
Name: "desktopicon"; Description: "Criar atalho na Área de Trabalho"; GroupDescription: "Atalhos:"

[Files]
; Copia todos os binários da pasta Release (inclusive o dbLivroReceita.mdf)
Source: "bin\Release\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{autoprograms}\LivroReceita"; Filename: "{app}\LivroReceita.exe"
Name: "{autodesktop}\LivroReceita"; Filename: "{app}\LivroReceita.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\LivroReceita.exe"; Description: "Iniciar LivroReceita"; Flags: nowait postinstall skipifsilent