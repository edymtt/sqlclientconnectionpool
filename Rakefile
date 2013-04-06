task :default => ["Start.exe"]

file "Start.exe" => ["Program\\Start.cs"] do
  sh "csc /out:Start.exe Program\\Start.cs"
end

task "nuget" do
  sh "powershell (.\\install.ps1)"
end