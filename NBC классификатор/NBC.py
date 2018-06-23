from sklearn import datasets
from sklearn.naive_bayes import GaussianNB
from sklearn.naive_bayes import MultinomialNB
import random
import numpy as np
import os
#iris = datasets.load_iris()
#f = open(r'C:\Users\Alisa\PycharmProjects\NBC\original_sings.txt')
f_original = open(r'C:\Users\Alisa\PycharmProjects\NBC\data266.txt')


data = []
target = []

#создать массив массивов data
for line in f_original.readlines():
    data.append(list(line.split()))

#все к float
data = list(map(lambda x: list(map(float, x)),data))
count_picture = len(data)
print("Количество всех изображений: ", count_picture, "\n")
print("Исходные значения признаков и метка класса в конце:\n",data,"\n")

#перемешать
random.shuffle(data)
print("Перемешивание строк:\n", data, "\n")

#извлечь последний элемент (метка класса: 0 - исходное, 1 - стего)
for i in range(len(data)):
   target.append(data[i].pop(-1))

print("Метки класса:\n", target, "\n")
print("Чистая data:\n", data, "\n")

f_original.close()

gnb = MultinomialNB()
y_pred = gnb.fit(data[:int(count_picture*0.5)], target[:int(count_picture*0.5)]).predict(data[int(count_picture*0.5):])
print(y_pred)
print("Количество ошибок классификатора из %d раз : %d" % (len(data[int(count_picture*0.5):]),
                                                                     (target[int(count_picture*0.5):] != y_pred).sum()))






