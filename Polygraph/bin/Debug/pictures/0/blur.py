from PIL import Image, ImageFilter
im = Image.open('standart.jpg')
mass = im.load()
x, y = im.size
im1 = im.filter(ImageFilter.GaussianBlur(radius=40))
im1.save('res.jpg')