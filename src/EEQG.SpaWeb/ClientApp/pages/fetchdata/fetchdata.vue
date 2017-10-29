<template>
    <div>
        <h1>Weather forecast 请求</h1>

        <p>This component demonstrates fetching data from the server.</p>
        <i-button type="primary" @click="getdata()">加载数据</i-button>
        <i-button type="primary" @click="changeaa()">改变aa</i-button>
        <table v-if="forecasts.length" class="table">
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Temp. (C)</th>
                    <th>Temp. (F)</th>
                    <th>Summary</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="item in forecasts">
                    <td>{{ item.dateFormatted }}</td>
                    <td>{{ item.temperatureC }}</td>
                    <td>{{ item.temperatureF }}</td>
                    <td>{{ item.summary }}</td>
                </tr>
            </tbody>
        </table>

        <p v-else><em>Loading...</em></p>
        <fetchc :ttt="aa"></fetchc>
        <div>{{$route.query}}</div>
    </div>
</template>

<script>
    import axios from 'axios';
    import fetchc from './fetchc.vue';
    export default  {
        name: 'FetchDataComponent',
        components: {
            fetchc
        },
        data() {
            return {
                aa:"2333333",
                forecasts: [],
                
            }
        },
        mounted: function () {
            this.getdata();
        },
        methods: {
            getdata: function() {
                axios.get('api/SampleData/WeatherForecasts')
                    .then(
                    response => {
                        this.forecasts = response.data;
                    })
            },
            changeaa() {
                this.aa = "34444";
            },
                
        }
    }
        
   
</script>
