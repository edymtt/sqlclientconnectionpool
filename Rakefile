require 'rake/clean'

CLOBBER.include('Test.dll')

task :default => ["Start.exe"]

file "Start.exe" => ["Program\\Start.cs"] do
  sh "csc /out:Start.exe Program\\Start.cs"
end

SRC = FileList['Program/*.cs'].gsub(%r|/|, "\\")

file "Test.dll" => SRC do
  sh "csc /target:library /out:Test.dll /lib:packages/NUnit/lib /r:nunit.framework.dll #{SRC}"
end

task "nuget" do
  sh "powershell (.\\install.ps1)"
end