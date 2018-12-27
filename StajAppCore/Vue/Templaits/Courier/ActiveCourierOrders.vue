<template id="temp2">
    <div class="main-content tabs-collor">
        <div class="col-12">
            <h1 class="font-weight-light text-justify">
                Здесь показаны все заказы взятые курьером
            </h1>
        </div>
        <br /><br />
        <div v-for="(item, i) in activeOrders" class="row">
            <div class="col-lg-3 col-md-3 col-sm-3"></div>
            <div v-if="item.userCancelled" class="col-lg-6 col-md-6 col-sm-6 active-order">
                <div class="form-group">
                    <h3 class="mt-4 text-danger">Пользователь отменил заказ :(</h3>
                </div>
                <div class="form-group">
                    <button @click="client = item.userOppositeSide" type="button" data-toggle="modal" data-target="#client-from-order" class="btn btn-warning">Показать пользователя</button>
                </div>
                <div class="form-group">
                    <button @click="Confirm(item.id)" class="btn btn-success">Закрыть доставку!</button>
                </div>
            </div>
            <div v-else class="col-lg-6 col-md-6 col-sm-6 active-order">
                <p>
                    Статус подтверждения доставки пользователем
                    <img v-if="item.deliveredOppositeSide" src="/images/GreenY.ico" width="35" height="35" />
                    <img v-else src="/images/RadN.png" width="35" height="35" />
                </p>
                <button @click="client = item.userOppositeSide" type="button" data-toggle="modal" data-target="#client-from-order" class="btn btn-warning">Показать пользователя</button>

                <div class="form-group">
                    <h5 class="mt-4">Комментарий к заказу</h5>
                    <textarea readonly rows="5" type="text" class="form-control" :placeholder="item.description">
                    </textarea>
                </div>
                <div class="form-group">
                    <h5 class="mt-2">Адрес доставки</h5>
                    <textarea readonly rows="3" type="text" class="form-control" :placeholder="item.deliveryAddress">
                    </textarea>
                </div>
                <div class="form-group">
                    <h5 class="mt-2">Список товаров</h5>
                    <select v-model="selecter[i]" class="form-control">
                        <option v-for="(element, index) in item.products" v-bind:value="index">{{element.name}}</option>
                    </select>
                    <div v-if="selecter[i] > -1">
                        <h5 class="mt-2">Товар №{{selecter[i] + 1}}</h5>
                        <label>{{item.products[selecter[i]].name}}</label>
                        <br />
                        <textarea readonly rows="3" type="text" class="form-control" :placeholder="item.products[selecter[i]].description">                 
                        </textarea>
                        <br />
                        <label>Цена: {{item.products[selecter[i]].price}}</label>
                        <br />

                        <label>Колличество: {{item.products[selecter[i]].count}} шт.</label>
                    </div>
                </div>
                <div class="form-group">
                    <button @click="Confirm(item.id)" class="btn btn-success">Заказ доставлен!</button>
                </div>
                <br />
                {{item.id}}
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3"></div>
        </div>

        <div class="modal fade modal-collor" id="client-from-order" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Информация о клиенте</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label>Имя: {{client.name}}</label>
                        </div>
                        <div class="form-group">
                            <label>Телефон: {{client.phone}}</label>
                        </div>
                        <div class="form-group">
                            <label>Email: {{client.email}}</label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    Vue.component('tab2', {
        template: '#temp2',
        data: function () {
            return {
                activeOrders: [],
                selecter: [],
                client: { }
            }
        },
        created() {
            this.GetOrders();
        },
        methods: {
            GetOrders: function () {
                axios.get('Courier/GetCourierOrders').
                    then(response => {
                        this.activeOrders = response.data;
                        console.log(this.activeOrders);
                    })
                    .catch(error => {
                        console.log(error);
                        HostApp.showMsg({
                            message: "Непредвиденная ошибка!!!"
                        });
                    })
            },
            Confirm: function (id) {
                axios.get('Courier/ConfirmOrderCourier/' + id).
                    then(response => {
                        this.GetOrders();
                        HostApp.showMsg(response.data);
                    })
                    .catch(error => {
                        console.log(error);
                        HostApp.showMsg({
                            message: "Непредвиденная ошибка!!!"
                        });
                    })
            }
        }
    })
</script>
