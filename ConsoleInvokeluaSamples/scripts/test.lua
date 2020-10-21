width=100
height=200
message="Hello World!"
color={r=100,g=20,b=50}
tree={branch1={leaf1=10,leaf2="leaf2"},leaf3="leaf3"}
array = {1,"string", 1.0,false,unixtime,os.date("%Y%m%d%H%M%S",unixtime),os.time({day=17, month=8, year=2018, hour=0, minute=0, second=0}),os.time()}

function func(x,y)
  return x,x+y
end

print("lua¥Ú”°")
print(unixtime)
print(os.time())
print(os.date("%Y-%m-%d %H:%M %S", os.time()))