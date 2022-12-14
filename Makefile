#publish:
#	dotnet publish -c Release -o LinqGen.Unity/Packages/com.cathei.linqgen -f netstandard2.1 LinqGen
#	dotnet publish -c Release -o LinqGen.Unity/Packages/com.cathei.linqgen LinqGen.Generator

copy:
	cp -R LinqGen/Src/* LinqGen.Unity/Packages/com.cathei.linqgen/Runtime
	dotnet publish -c Release -o LinqGen.Unity/Packages/com.cathei.linqgen LinqGen.Generator

