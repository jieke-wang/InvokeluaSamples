-- require("<模块名>")
--print(package.path)
-- 添加package目录的搜寻路径
package.path = package.path..";./scripts/?.lua"
--print(package.path)
module_Sample = require("module_Sample")

print(module_Sample.constant)
module_Sample.func3()
