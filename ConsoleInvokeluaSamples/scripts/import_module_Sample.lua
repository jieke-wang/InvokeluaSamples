-- require("<ģ����>")
--print(package.path)
-- ���packageĿ¼����Ѱ·��
package.path = package.path..";./scripts/?.lua"
--print(package.path)
module_Sample = require("module_Sample")

print(module_Sample.constant)
module_Sample.func3()
