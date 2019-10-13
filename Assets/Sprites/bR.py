import os
from PIL import Image


path = input('Enter the path of the folder on which you want to perform this action: ')
os.chdir(path)

directory = input('Enter directory Name: ')

output = input('Enter resultant directory Name: ')
try:
    os.umask(0)
    os.mkdir(output)
except:
     pass   


for filename in os.listdir('./'+directory+'/'):
    print(filename)
    img = Image.open(directory+'/'+filename)
    img = img.convert("RGBA")
    datas = img.getdata()

    newData = []
    for item in datas:
        if item[0] == 255 and item[1] == 255 and item[2] == 255:
            newData.append((255, 255, 255, 0))
        else:
            if item[0] > 150:
                newData.append((0, 0, 0, 255))
            else:
                newData.append(item)
                


    img.putdata(newData)
    img.save(output+'/'+filename)
print('done')
