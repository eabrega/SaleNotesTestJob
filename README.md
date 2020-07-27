# SaleNotesTestJob
Тестовое задание

 В небольшом магазине инструментов работает менеджер по продажам. Ему, как и всем менеджерам, необходимо вести простейший учет: что, когда, кому, по какой цене и в каком кол-ве он продал.
Вам же нужно разработать программное обеспечение (библиотека и простой UI), которое позволит вести такой учет.

Требования реализации:

Спроектируйте классы предметной области (Заказ, Строка заказа, Клиент - какие-то еще на Ваше усмотрение), но учтите, что в будущем они могут быть использованы при разработке новых версий приложения.

Необходимо также чтобы наш менеджер по продажам мог бы получать следующие отчеты:

а. Отчет за год, в котором бы была следующая информация по месяцам:
- Сумма продаж за месяц
- Средний чек
- Самый продаваемый товар
- Клиент, который большее кол-во дней бывал и покупал что-то в магазине. 

б. Отчет за год, в котором бы была следующая информация по клиентам
- Сумма, которую клиент потратил за год
- Сумма среднего чека клиента за год
- Максимальная сумма среднего чека за месяц
- Номер месяца, когда сумма среднего чека была максимальной

в. Отчет со списком клиентов, которые ранее в нашем магазине купили молоток или шуруповерт, но не покупали гвозди или шурупы уже на протяжении 2х месяцев. В отчете должна быть информация - что именно так давно не покупал клиент.

Для реализации предусмотрите тестовую выборку данных с заказами пользователям: можно создать экземпляры в памяти, можно хранить данные в виде файлов  JSON, XML - или любым другим удобным способом. 

Интерфейс пользователя должен быть построен как консольное приложение или  Windows Forms. Последнее будет большим преимуществом. Детали реализации интерфейса остаются на ваше усмотрение.
