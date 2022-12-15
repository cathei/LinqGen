#publish:
#	dotnet publish -c Release -o LinqGen.Unity/Packages/com.cathei.linqgen -f netstandard2.1 LinqGen
#	dotnet publish -c Release -o LinqGen.Unity/Packages/com.cathei.linqgen LinqGen.Generator

copy:
	cp -R LinqGen/Core/* LinqGen.Unity/Packages/com.cathei.linqgen/Runtime/Core
	dotnet publish -c Release -o LinqGen.Unity/Packages/com.cathei.linqgen LinqGen.Generator

