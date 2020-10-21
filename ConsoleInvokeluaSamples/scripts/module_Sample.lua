-- 文件名为 module.lua
-- 定义一个名为 module_Sample 的模块
module_Sample = {}

-- 定义一个常量
module_Sample.constant = "这是一个常量"

-- 定义一个函数
function module_Sample.func1()
    io.write("这是一个公有函数！\n")
end

local function func2()
    print("这是一个私有函数！")
end

function module_Sample.func3()
    func2()
end

return module_Sample