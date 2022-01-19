* Решить проблему с null, приходящими из VM после изменения структуры наследования [РЕШЕНО]
* Проект не должен всегда содержать ConcretePerimeter = 0 после добавления. Необходимо инициализировать либо
ConcretePerimeter, если он равен 0 и сделать его равным CommonPerimeter - NonConcretePerimeter, либо если NonConretePerimeter = 0,
то проинициализировать его как CommonPerimeter - ConcreterPerimeter
