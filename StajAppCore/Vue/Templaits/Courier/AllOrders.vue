<template id="temp1">
    <div class="main-content tabs-collor">
        <div class="col-12">
            <h1 class="font-weight-light text-justify">
                Здесь показаны все активные заказы всех пользователей
            </h1>
            <br /><br />
            <div v-for="(item, i) in (Math.floor(activeOrders.length / 3) + 1)">
                <div class="row">
                    <div v-for="(el, ind) in 3" v-if="activeOrders.length > i * 3 + ind" class="col-lg-4 col-md-4 col-sm-4 active-order">
                        <div class="form-group">
                            <h5 class="mt-4">Комментарий к заказу</h5>
                            <textarea readonly rows="5" type="text" class="form-control" :placeholder="activeOrders[i * 3 + ind].description">
                            </textarea>
                        </div>
                        <div class="form-group">
                            <h5 class="mt-2">Адрес доставки</h5>
                            <textarea readonly rows="3" type="text" class="form-control" :placeholder="activeOrders[i * 3 + ind].deliveryAddress">
                            </textarea>
                        </div>

                        <div class="form-group">
                            <h5 class="mt-2">Список товаров</h5>
                            <select v-model="selecter[i * 3 + ind]" class="form-control">
                                <option v-for="(prod, iProd) in activeOrders[i * 3 + ind].products" v-bind:value="iProd">{{prod.name}}</option>
                            </select>
                            <div v-if="selecter[i * 3 + ind] > -1">
                                <h5 class="mt-2">Товар №{{selecter[i * 3 + ind] + 1}}</h5>
                                <label>{{activeOrders[i * 3 + ind].products[selecter[i * 3 + ind]].name}}</label>
                                <br />
                                <textarea readonly rows="3" type="text" class="form-control" :placeholder="activeOrders[i * 3 + ind].products[selecter[i * 3 + ind]].description">                 
                                </textarea>
                                <br />
                                <label>Цена: {{activeOrders[i * 3 + ind].products[selecter[i * 3 + ind]].price}}</label>
                                <br />

                                <label>Колличество: {{activeOrders[i * 3 + ind].products[selecter[i * 3 + ind]].count}} шт.</label>
                            </div>
                        </div>

                        <div class="form-group">
                            <button @click="client = activeOrders[i * 3 + ind].userOppositeSide" type="button" data-toggle="modal" data-target="#user-from-order" class="btn btn-warning">Показать пользователя</button>
                        </div>

                        <div class="form-group">
                            <button @click="TakeOrder(activeOrders[i * 3 + ind].id)" class="btn btn-success">Взять заказ!</button>
                        </div>
                        <br />
                        {{activeOrders[i * 3 + ind].id}}
                    </div>
                </div>
            </div>

            <div class="modal fade modal-collor" id="user-from-order" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
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
    </div>
</template>

<script>
    Vue.component('AllOrders', {
        template: '#temp1',
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
                axios.get('GetAllOrders').
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
            TakeOrder: function (id) {
                axios.get('TakeOrder/' + id).
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
