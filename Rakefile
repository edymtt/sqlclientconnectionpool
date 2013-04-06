require 'rake/clean'

CLOBBER.include('Test.dll')

task :default => ["test"]

SRC = FileList['Program/*.cs'].gsub(%r|/|, "\\")

file "Test.dll" => SRC do
  sh "copy packages\\NUnit\\lib\\nunit.framework.dll ."
  sh "csc /target:library /out:Test.dll /r:nunit.framework.dll #{SRC}"
end

task "test" => ["nuget", "Test.dll"] do
  sh "packages\\NUnit.Runners\\tools\\nunit-console.exe Test.dll"
end

task "nuget" do
  sh "powershell (.\\install.ps1)"
end

task "provisiondb" do
  sh "powershell (.\\provision_database.ps1  lappy8\\sqlexpress)"
end