#publish:
#	dotnet publish -c Release -o LinqGen.Unity/Packages/com.cathei.linqgen -f netstandard2.1 LinqGen
#	dotnet publish -c Release -o LinqGen.Unity/Packages/com.cathei.linqgen LinqGen.Generator

copy:
	cp -R LinqGen/Core/* LinqGen.Unity/Packages/com.cathei.linqgen/Runtime/Core
	dotnet publish -c Release -o LinqGen.Unity/Packages/com.cathei.linqgen LinqGen.Generator

benchmark:
	dotnet clean -c Release
	dotnet run --project LinqGen.Benchmarks -c Release -f net6.0

benchmark-net7:
	dotnet clean -c Release
	dotnet run --project LinqGen.Benchmarks -c Release -f net7.0

benchmark-mono:
	dotnet clean -c Release
	dotnet run --project LinqGen.Benchmarks -c Release -f net472


