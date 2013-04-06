require 'rake/clean'

CLOBBER.include('Test.dll')

task :default => ["Start.exe"]

file "Start.exe" => ["Program\\Start.cs"] do
  sh "csc /out:Start.exe Program\\Start.cs"
end

SRC = FileList['Program/*.cs'].gsub(%r|/|, "\\")

file "Test.dll" => SRC do
  sh "copy packages\\NUnit\\lib\\nunit.framework.dll ."
  sh "csc /target:library /out:Test.dll /r:nunit.framework.dll #{SRC}"
end

task "test" => ["Test.dll"] do
  sh "packages\\NUnit.Runners\\tools\\nunit-console.exe Test.dll"
end

task "nuget" do
  sh "powershell (.\\install.ps1)"
end