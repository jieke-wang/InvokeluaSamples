-- �ļ���Ϊ module.lua
-- ����һ����Ϊ module_Sample ��ģ��
module_Sample = {}

-- ����һ������
module_Sample.constant = "����һ������"

-- ����һ������
function module_Sample.func1()
    io.write("����һ�����к�����\n")
end

local function func2()
    print("����һ��˽�к�����")
end

function module_Sample.func3()
    func2()
end

return module_Sample