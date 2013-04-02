task :default => ["Start.exe"]

file "Start.exe" => ["Program\\Start.cs"] do
  sh "csc /out:Start.exe Program\\Start.cs"
end

task "nugetup" do
  #sh "md packages"
  sh "nuget update packages.config -RepositoryPath packages"
end

task "nugetinit" do
  sh "nuget install .\packages.config -o packages"
end