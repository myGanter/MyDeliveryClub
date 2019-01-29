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

                                    <div v-if="showResultMsg">
                                        <p class="err" v-for="e in resultMsg['Description']">
                                            {{ e }}
                                        </p>
                                    </div>

                                    <textarea rows="3" type="text" v-model="order.Description" class="form-control" id="desc"></textarea>
                                </div>
                                <div class="form-group">
                                    <label for="addres">Адрес доставки</label>

                                    <div v-if="showResultMsg">
                                        <p class="err" v-for="e in resultMsg['DeliveryAddress']">
                                            {{ e }}
                                        </p>
                                    </div>

                                    <input type="text" v-model="order.DeliveryAddress" class="form-control" id="addres">
                                </div>

                                <div id="ordersBlock">
                                    <div id="order1">
                                        <h3>Товар №1</h3>
                                        <div class="form-group">
                                            <label>Название товара</label>

                                            <div v-if="showResultMsg">
                                                <p class="err" v-for="e in resultMsg['Products[0].Name']">
                                                    {{ e }}
                                                </p>
                                            </div>

                                            <input type="text" v-model="order.Products[0].Name" class="form-control">
                                        </div>
                                        <div class="form-group">
                                            <label>Описание товара (Укажите адрес и название магазина)</label>

                                            <div v-if="showResultMsg">
                                                <p class="err" v-for="e in resultMsg['Products[0].Description']">
                                                    {{ e }}
                                                </p>
                                            </div>

                                            <textarea rows="3" type="text" v-model="order.Products[0].Description" class="form-control"></textarea>
                                        </div>
                                        <div class="form-group">
                                            <label>Цена</label>
                                            <select v-model="order.Products[0].Price" class="form-control">
                                                <option value="100" selected>до 100</option>
                                                <option value="1000">до 1000</option>
                                                <option value="10000">до 10000</option>
                                            </select>                                            
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
    Vue.component('Shops', {
        template: '#temp1',
        data: function () {
            return {
                order:
                {
                    Description: '',
                    DeliveryAddress: '',
                    Products: [{ Name: '', Description: '', Price: 100, Count: 1 }]
                },
                orderCounter: 1,
                resultMsg: {},
                showResultMsg: false
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
                        '<div class="errname"></div>' +
                        '<input id="nameOrder' + this.orderCounter + '" type="text" class="form-control"> </div>' +
                        '<div class="form-group"> <label>Описание товара (Укажите адрес и название магазина)</label>' +
                        '<div class="errdescription"></div>' +
                        '<textarea rows="3" id="descriptionOrder' + this.orderCounter + '" type="text" class="form-control"></textarea> </div>' +
                        '<div class="form-group"> <label>Цена</label>' +
                        '<select id="priceOrder' + this.orderCounter + '" class="form-control">' +
                        '<option value="100" selected>до 100</option>' +
                        '<option value="1000">до 1000</option>' +
                        '<option value="10000">до 10000</option>' +
                        '</select></div>' +
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
                axios.post('AddUniqOrder',
                    this.order
                ).
                    then(response => {
                        console.log(response);

                        this.showResultMsg = false;
                        $(".errname").html('');
                        $(".errdescription").html('');
                        if (response.data.errors !== null) {
                            this.resultMsg = response.data.errors;
                            this.showResultMsg = true;
                            this.addErr(".errname", 'Name');
                            this.addErr(".errdescription", 'Description');
                        }                            

                        HostApp.showMsg(response.data); 
                    })
                    .catch(error => {
                        console.log(error);
                        HostApp.showMsg({
                            message: "Непредвиденная ошибка!!!"
                        });
                    })
                this.order = {
                    Description: this.order.Description,
                    DeliveryAddress: this.order.DeliveryAddress,
                    Products: [this.order.Products[0]]
                };
            },
            addErr: function (el, prop) {
                var els = $(el);
                for (var i = 0; i < els.length; i++) {
                    var ii = i + 1;
                    if (typeof this.resultMsg['Products[' + ii + '].' + prop] !== "undefined")
                        for (var j = 0; j < this.resultMsg['Products[' + ii + '].' + prop].length; j++)
                            $(els[i]).append('<p class="err">' + this.resultMsg['Products[' + ii + '].' + prop][j] + '</p>');
                }
            }
        }
    })
</script>
