<template id="temp2">
    <div class="main-content tabs-collor">
        <div class="col-12">
            <p class="font-weight-light text-justify">
                Равным образом постоянное информационно-техническое обеспечение нашей деятельности требует от нас анализа системы обучения кадров, соответствующей насущным потребностям? Повседневная практика показывает, что дальнейшее развитие различных форм деятельности создаёт предпосылки качественно новых шагов для позиций, занимаемых участниками в отношении поставленных задач! Практический опыт показывает, что начало повседневной работы по формированию позиции позволяет выполнить важнейшие задания по разработке системы масштабного изменения ряда параметров?

                Практический опыт показывает, что курс на социально-ориентированный национальный проект способствует повышению актуальности существующих финансовых и административных условий? 
            </p>
        </div>
        <br/><br/>
        <div v-for="(item, i) in activeOrders" class="row">
            <div class="col-lg-3 col-md-3 col-sm-3"></div>
            <div class="col-lg-6 col-md-6 col-sm-6 active-order">
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
                    <button @click="Confirm(item.id)" class="btn btn-success">Заказ принят!</button>
                </div>
                <br />
                {{item.id}}
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3"></div>                  
        </div>
    </div>
</template>

<script>
    Vue.component('tab2', {
        template: '#temp2',
        data: function () {
            return {
                activeOrders: [],
                selecter: []
            }
        },
        created() {
            this.GetOrders();
        },
        methods: {
            GetOrders: function () {
                axios.get('User/GetUserOrders').
                    then(response => {
                        this.activeOrders = response.data;
                        console.log(this.activeOrders);
                    })
                    .catch(error => {
                        console.log(error);
                        alert('Авторизируйтесь под юзером');
                    })
            },
            Confirm: function (id) {
                axios.get('User/ConfirmOrder/' + id).
                    then(response => {
                        this.GetOrders();
                        console.log(response.data);
                    })
                    .catch(error => {
                        console.log(error);
                    })
            }
        }
    })
</script>
