del "ProjectTemplates\Visual Studio 2010\CSharp\1033.zip"
del "ProjectTemplates\Visual Studio 2010\CSharp\1049.zip"

del "ProjectTemplates\Visual Studio 2012\CSharp\1033.zip"
del "ProjectTemplates\Visual Studio 2012\CSharp\1049.zip"


del "ItemTemplates\Visual Studio 2010\CSharp\1033.zip"
del "ItemTemplates\Visual Studio 2010\CSharp\1049.zip"

del "ItemTemplates\Visual Studio 2012\CSharp\1033.zip"
del "ItemTemplates\Visual Studio 2012\CSharp\1049.zip"

"..\..\..\Tools and Resources\Utilities\7za\7za.exe" a "ProjectTemplates\Visual Studio 2010\CSharp\1033.zip" ".\ProjectTemplates\Visual Studio 2010\CSharp\1033\*" -x!*ProjectTemplate.csproj -x!*.vspscc -x!*\ -x!*.tt
"..\..\..\Tools and Resources\Utilities\7za\7za.exe" a "ProjectTemplates\Visual Studio 2010\CSharp\1049.zip" ".\ProjectTemplates\Visual Studio 2010\CSharp\1049\*" -x!*ProjectTemplate.csproj -x!*.vspscc -x!*\ -x!*.tt

"..\..\..\Tools and Resources\Utilities\7za\7za.exe" a "ProjectTemplates\Visual Studio 2012\CSharp\1033.zip" ".\ProjectTemplates\Visual Studio 2012\CSharp\1033\*" -x!*ProjectTemplate.csproj -x!*.vspscc -x!*\ -x!*.tt
"..\..\..\Tools and Resources\Utilities\7za\7za.exe" a "ProjectTemplates\Visual Studio 2012\CSharp\1049.zip" ".\ProjectTemplates\Visual Studio 2012\CSharp\1049\*" -x!*ProjectTemplate.csproj -x!*.vspscc -x!*\ -x!*.tt


"..\..\..\Tools and Resources\Utilities\7za\7za.exe" a "ItemTemplates\Visual Studio 2010\CSharp\1033.zip" ".\ItemTemplates\Visual Studio 2010\CSharp\1033\*" -x!*ItemTemplate.csproj -x!*.vspscc -x!*\ -x!*.tt
"..\..\..\Tools and Resources\Utilities\7za\7za.exe" a "ItemTemplates\Visual Studio 2010\CSharp\1049.zip" ".\ItemTemplates\Visual Studio 2010\CSharp\1049\*" -x!*ItemTemplate.csproj -x!*.vspscc -x!*\ -x!*.tt

"..\..\..\Tools and Resources\Utilities\7za\7za.exe" a "ItemTemplates\Visual Studio 2012\CSharp\1033.zip" ".\ItemTemplates\Visual Studio 2012\CSharp\1033\*" -x!*ItemTemplate.csproj -x!*.vspscc -x!*\ -x!*.tt
"..\..\..\Tools and Resources\Utilities\7za\7za.exe" a "ItemTemplates\Visual Studio 2012\CSharp\1049.zip" ".\ItemTemplates\Visual Studio 2012\CSharp\1049\*" -x!*ItemTemplate.csproj -x!*.vspscc -x!*\ -x!*.tt

pause 