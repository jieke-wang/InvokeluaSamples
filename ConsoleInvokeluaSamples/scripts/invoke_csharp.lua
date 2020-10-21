luanet.load_assembly('ConsoleInvokeluaSamples', 'ConsoleInvokeluaSamples.CSharpNamespace')
CSharpClass=luanet.import_type('ConsoleInvokeluaSamples.CSharpNamespace.CSharpClass')

res1=CSharpClass.CSharpStaticMethod("a","b","c")
print(res1)

instance=CSharpClass(1,2)
res2=instance:CSharpInstanceMethod(3)
print(res2)

return res1,res2