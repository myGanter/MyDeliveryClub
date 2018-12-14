<template id="temp1">
    <div class="main-content tabs-collor">
        <div class="row">
            <div class="col-lg-3 col-md-3 col-sm-3">
                <img src="/images/default/DefaultShop.png" width="150" height="150" />
                <p class="h4">Магазин name</p>
                <button type="button" class="btn btn-outline-success">Каталог товаров</button>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3">
                <img src="/images/default/DefaultShop.png" width="150" height="150" />
                <p class="h4">Магазин name</p>
                <button type="button" class="btn btn-outline-success">Каталог товаров</button>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3">
                <img src="/images/default/DefaultShop.png" width="150" height="150" />
                <p class="h4">Магазин name</p>
                <button type="button" class="btn btn-outline-success">Каталог товаров</button>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3">
                <img src="/images/default/DefaultShop.png" width="150" height="150" />
                <p class="h4">Магазин name</p>
                <button type="button" class="btn btn-outline-success">Каталог товаров</button>
            </div>
        </div>

        <div class="row">
            <div class="col-lg-3 col-md-3 col-sm-3">
                <img src="/images/default/DefaultShop.png" width="150" height="150" />
                <p class="h4">Магазин name</p>
                <button type="button" class="btn btn-outline-success">Каталог товаров</button>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3">
                <img src="/images/default/DefaultShop.png" width="150" height="150" />
                <p class="h4">Магазин name</p>
                <button type="button" class="btn btn-outline-success">Каталог товаров</button>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3">
                <img src="/images/default/DefaultShop.png" width="150" height="150" />
                <p class="h4">Магазин name</p>
                <button type="button" class="btn btn-outline-success">Каталог товаров</button>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3">
                <img src="/images/default/DefaultShop.png" width="150" height="150" />
                <p class="h4">Магазин name</p>
                <button type="button" class="btn btn-outline-success">Каталог товаров</button>
            </div>
        </div>        

        <div class="row order-form">
            <div class="col-12">
                <p class="h5">Не нашли нужный магазин?</p>
                <button type="button" data-toggle="modal" data-target="#order-uniq-form" class="btn btn-success">Создайте уникальную заявку!</button>

                <div class="modal fade modal-collor" id="order-uniq-form" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel">Оформление заказа</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <label for="desc">Ваш комментарий</label>
                                    <textarea rows="3" type="text" v-model="order.Description" class="form-control" id="desc"></textarea>
                                </div>
                                <div class="form-group">
                                    <label for="addres">Адрес доставки</label>
                                    <input type="text" v-model="order.DeliveryAddress" class="form-control" id="addres">
                                </div>

                                <div id="ordersBlock">
                                    <div id="order1">
                                        <h3>Товар №1</h3>
                                        <div class="form-group">
                                            <label>Название товара</label>
                                            <input type="text" v-model="order.Products[0].Name" class="form-control">
                                        </div>
                                        <div class="form-group">
                                            <label>Описание товара (Укажите адрес и название магазина)</label>                                 
                                            <textarea rows="3" type="text" v-model="order.Products[0].Description" class="form-control"></textarea>
                                        </div>
                                        <div class="form-group">
                                            <label>Цена</label>
                                            <input type="text" v-model="order.Products[0].Price" class="form-control">
                                        </div>
                                        <div class="form-group">
                                            <label>Колличество : {{order.Products[0].Count}} шт.</label>
                                            <button type="button" @click="RemoveCountProduct(0)" class="btn btn-outline-danger">-</button>
                                            <button type="button" @click="AddCountProduct(0)" class="btn btn-outline-success">+</button>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <button @click="AddProduct()" class="btn btn-success">Добавить товар</button>
                                </div>
                                <div class="form-group">
                                    <button @click="AddUniqOrder()" data-dismiss="modal" class="btn btn-primary">Отправить заявку</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</template>

<script>
    Vue.component('tab1', {
        template: '#temp1',
        data: function () {
            return {
                order:
                {
                    Description: '',
                    DeliveryAddress: '',
                    Products: [{ Name: '', Description: '', Price: 1, Count: 1 }]
                },
                orderCounter: 1
            }
        },
        methods: {
            AddProduct: function () {
                this.orderCounter++;
                $('#ordersBlock').append
                    (
                    '<div id="order' + this.orderCounter + '">' +
                        '<h3>Товар №' + this.orderCounter + '</h3>' +
                        '<div class="form-group"> <button id="delBatt' + this.orderCounter + '" type="button" class="btn btn-outline-danger">Удалить товар</button> </div>' +
                        '<div class="form-group"> <label>Название товара</label>' +
                        '<input id="nameOrder' + this.orderCounter + '" type="text" class="form-control"> </div>' +
                        '<div class="form-group"> <label>Описание товара (Укажите адрес и название магазина)</label>' +
                        '<textarea rows="3" id="descriptionOrder' + this.orderCounter + '" type="text" class="form-control"></textarea> </div>' +
                        '<div class="form-group"> <label>Цена</label>' +
                        '<input id="priceOrder' + this.orderCounter + '" type="text" value="1" class="form-control"> </div>' +
                        '<div class="form-group"> <label>Колличество : <label id="countOrder' + this.orderCounter + '">1</label> шт.</label>' +
                        '<button type="button" id="removeCout' + this.orderCounter + '" class="btn btn-outline-danger mr-1">-</button>' +
                        '<button type="button" id="addCout' + this.orderCounter + '" class="btn btn-outline-success">+</button>' +
                    '</div>'
                );
                $('#delBatt' + this.orderCounter).click(this.orderCounter, function (eventObject) {
                    $('#order' + eventObject.data).remove();
                });
                $('#removeCout' + this.orderCounter).click(this.orderCounter, function (eventObject) {
                    var value = Number.parseInt($('#countOrder' + eventObject.data).text());
                    if (value > 1)
                    {
                        value--;
                        $('#countOrder' + eventObject.data).text(value);
                    }
                });
                $('#addCout' + this.orderCounter).click(this.orderCounter, function (eventObject) {
                    var value = Number.parseInt($('#countOrder' + eventObject.data).text());                    
                    value++;
                    $('#countOrder' + eventObject.data).text(value);                   
                });                
            },
            AddCountProduct: function (i) {
                this.order.Products[i].Count++;
            },
            RemoveCountProduct: function (i) {
                if (this.order.Products[i].Count > 1)
                    this.order.Products[i].Count--;
            },
            AddUniqOrder: function () {
                for (var i = 2; i <= this.orderCounter; i++)
                    if ($('#order' + i).length) {
                        this.order.Products.push({
                            Name: $('#nameOrder' + i).val(),
                            Description: $('#descriptionOrder' + i).val(),
                            Price: $('#priceOrder' + i).val(),
                            Count: $('#countOrder' + i).text()
                        });
                    }
                console.log(this.order);        
                axios.post('User/AddUniqOrder',
                    this.order
                ).
                    then(response => {
                        console.log(response);
                        alert(response.data.error ? 'Ошибка' : 'Заказ добавлен!');
                    })
                    .catch(error => {
                        console.log(error);
                        alert('Авторизируйтесь под юзером');
                    })
            }
        }
    })
</script>
