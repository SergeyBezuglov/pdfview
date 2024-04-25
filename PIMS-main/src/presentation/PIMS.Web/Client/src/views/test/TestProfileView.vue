<template>
    <v-breadcrumbs :items="['Личный кабинет']" class="main-breadcrumbs" bg-color="secondary"></v-breadcrumbs>
    <v-form>
        <v-container variant="outlined">
            <v-row justify="space-between">
                <v-col cols="3">
                    <v-avatar class="profile-user-icon mr-3 border-primary" color="secondary" size="45">
                        <v-icon color="primary">fa-solid fa-user-tie</v-icon>
                    </v-avatar>
                    <div class="profile-user-data-box">
                        <p class="profile-user-name">Володин Е.В.</p>
                        <p class="profile-user-login my-2">tmk\maslenikov</p>
                    </div>
                </v-col>

                <v-col cols="3">
                    <v-row>
                        <v-col cols="12" class="d-flex justify-end">
                            <div class="profile-menu border-round pa-1 border-secondary">
                                <v-btn-toggle
                                    v-model="toggle_menu"
                                >
                                    <v-btn class="profile-menu-item"
                                    color="purple-darken-2"
                                    @click="overlay = !overlay">
                                        <span>Уведомления</span>
                                    </v-btn>
                                    <v-overlay
                                    :model-value="overlay"
                                    class="align-center justify-center"
                                    >
                                    <v-card
                                    color="primary"
                                    >
                                    <v-card-text>
                                        Ожидайте
                                        <v-progress-linear
                                        indeterminate
                                        color="white"
                                        class="mb-0"
                                        ></v-progress-linear>
                                    </v-card-text>
                                    </v-card>
                                </v-overlay>
                                    <v-btn class="profile-menu-item">
                                        <span>Настройки</span>
                                    </v-btn>
                                </v-btn-toggle>
                            </div>
                        </v-col>
                    </v-row>
                </v-col>
            </v-row>

            <v-row class="my-0">
                <v-col cols="12" class="profile-slider-container">
                    <h3 class="title-article" color="title_article">Документы на подписание</h3>
                    <v-slide-group v-model="model" center-active show-arrows>
                        <v-slide-group-item>
                            <v-sheet class="profile-slider-item border-round border-secondary bg-green-gradient">
                                <v-row justify="space-between">
                                    <v-col cols="6">
                                        <v-chip
                                            class="profile-slider-item-topic"
                                            color="success"
                                          >Согласование</v-chip>
                                    </v-col>
                                    <v-col cols="1">
                                        <v-icon class="icon-15px" color="success">fa-regular fa-handshake</v-icon>
                                    </v-col>
                                </v-row>
                                <v-row >
                                    <v-col cols="12">
                                        <p class="profile-slider-item-title">Распоряжение № 138 от 01.11.2022 "Тест РСП-ДЭБ 0111"</p>
                                    </v-col>
                                </v-row>
                                <v-row justify="space-between" class="profile-slider-item-footer">
                                    <v-col cols="4">
                                        <v-icon class="profile-slider-item-date-icon icon-15px">fa-solid fa-calendar-day</v-icon>
                                        <span class="profile-slider-item-date">29.02.2023</span>
                                    </v-col>
                                    <v-col cols="4">
                                        <a href="#" class="btn btn-sm btn-info profile-slider-item-link">
                                            Перейти
                                        </a>
                                    </v-col>
                                </v-row>
                            </v-sheet>
                        </v-slide-group-item>

                        <v-slide-group-item>
                            <v-sheet class="profile-slider-item border-round border-secondary bg-orange-gradient">
                                <v-row justify="space-between">
                                    <v-col cols="6">
                                        <v-chip
                                            class="profile-slider-item-topic"
                                            color="warning"
                                          >Ознакомление</v-chip>
                                    </v-col>
                                    <v-col cols="1">
                                        <v-icon class="icon-15px" color="warning">fa-solid fa-glasses</v-icon>
                                    </v-col>
                                </v-row>
                                <v-row>
                                    <v-col cols="12">
                                        <p class="profile-slider-item-title">Проверка знаний на 2023 г.</p>
                                    </v-col>
                                </v-row>
                                <v-row justify="space-between" class="profile-slider-item-footer">
                                    <v-col cols="4">
                                        <v-icon class="profile-slider-item-date-icon icon-15px">fa-solid fa-calendar-day</v-icon>
                                        <span class="profile-slider-item-date">01.01.2023</span>
                                    </v-col>
                                    <v-col cols="4">
                                        <a href="#" class="btn btn-sm btn-info profile-slider-item-link">
                                            Перейти
                                        </a>
                                    </v-col>
                                </v-row>
                            </v-sheet>
                        </v-slide-group-item>

                        <v-slide-group-item>
                            <v-sheet class="profile-slider-item border-round border-secondary bg-blue-gradient">
                                <v-row justify="space-between">
                                    <v-col cols="6">
                                        <v-chip
                                            class="profile-slider-item-topic"
                                            color="info"
                                          >Подписание</v-chip>
                                    </v-col>
                                    <v-col cols="1">
                                        <v-icon class="icon-15px" color="info">fa-solid fa-feather</v-icon>
                                    </v-col>
                                </v-row>
                                <v-row >
                                    <v-col cols="12">
                                        <p class="profile-slider-item-title">График проверки знаний на 2023 г.</p>
                                    </v-col>
                                </v-row>
                                <v-row justify="space-between" class="profile-slider-item-footer">
                                    <v-col cols="4">
                                        <v-icon class="profile-slider-item-date-icon icon-15px">fa-solid fa-calendar-day</v-icon>
                                        <span class="profile-slider-item-date">29.02.2023</span>
                                    </v-col>
                                    <v-col cols="4">
                                        <a href="#" class="btn btn-sm btn-info profile-slider-item-link">
                                            Перейти
                                        </a>
                                    </v-col>
                                </v-row>
                            </v-sheet>
                        </v-slide-group-item>

                        <v-slide-group-item>
                            <v-sheet class="profile-slider-item border-round border-secondary bg-green-gradient">
                                <v-row justify="space-between">
                                    <v-col cols="6">
                                        <v-chip
                                            class="profile-slider-item-topic"
                                            color="success"
                                          >Согласование</v-chip>
                                    </v-col>
                                    <v-col cols="1">
                                        <v-icon class="icon-15px" color="success">fa-regular fa-handshake</v-icon>
                                    </v-col>
                                </v-row>
                                <v-row >
                                    <v-col cols="12">
                                        <p class="profile-slider-item-title">Распоряжение № 138 от 01.11.2022 "Тест РСП-ДЭБ 0111"</p>
                                    </v-col>
                                </v-row>
                                <v-row justify="space-between" class="profile-slider-item-footer">
                                    <v-col cols="4">
                                        <v-icon class="profile-slider-item-date-icon icon-15px">fa-solid fa-calendar-day</v-icon>
                                        <span class="profile-slider-item-date">29.02.2023</span>
                                    </v-col>
                                    <v-col cols="4">
                                        <a href="#" class="btn btn-sm btn-info profile-slider-item-link">
                                            Перейти
                                        </a>
                                    </v-col>
                                </v-row>
                            </v-sheet>
                        </v-slide-group-item>

                        <v-slide-group-item>
                            <v-sheet class="profile-slider-item border-round border-secondary bg-orange-gradient">
                                <v-row justify="space-between">
                                    <v-col cols="6">
                                        <v-chip
                                            class="profile-slider-item-topic"
                                            color="warning"
                                          >Ознакомление</v-chip>
                                    </v-col>
                                    <v-col cols="1">
                                        <v-icon class="icon-15px" color="warning">fa-solid fa-glasses</v-icon>
                                    </v-col>
                                </v-row>
                                <v-row>
                                    <v-col cols="12">
                                        <p class="profile-slider-item-title">Проверка знаний на 2023 г.</p>
                                    </v-col>
                                </v-row>
                                <v-row justify="space-between" class="profile-slider-item-footer">
                                    <v-col cols="4">
                                        <v-icon class="profile-slider-item-date-icon icon-15px">fa-solid fa-calendar-day</v-icon>
                                        <span class="profile-slider-item-date">01.01.2023</span>
                                    </v-col>
                                    <v-col cols="4">
                                        <a href="#" class="btn btn-sm btn-info profile-slider-item-link">
                                            Перейти
                                        </a>
                                    </v-col>
                                </v-row>
                            </v-sheet>
                        </v-slide-group-item>

                        <v-slide-group-item>
                            <v-sheet class="profile-slider-item border-round border-secondary bg-blue-gradient">
                                <v-row justify="space-between">
                                    <v-col cols="6">
                                        <v-chip
                                            class="profile-slider-item-topic"
                                            color="info"
                                          >Подписание</v-chip>
                                    </v-col>
                                    <v-col cols="1">
                                        <v-icon class="icon-15px" color="info">fa-solid fa-feather</v-icon>
                                    </v-col>
                                </v-row>
                                <v-row >
                                    <v-col cols="12">
                                        <p class="profile-slider-item-title">График проверки знаний на 2023 г.</p>
                                    </v-col>
                                </v-row>
                                <v-row justify="space-between" class="profile-slider-item-footer">
                                    <v-col cols="4">
                                        <v-icon class="profile-slider-item-date-icon icon-15px">fa-solid fa-calendar-day</v-icon>
                                        <span class="profile-slider-item-date">29.02.2023</span>
                                    </v-col>
                                    <v-col cols="4">
                                        <a href="#" class="btn btn-sm btn-info profile-slider-item-link">
                                            Перейти
                                        </a>
                                    </v-col>
                                </v-row>
                            </v-sheet>
                        </v-slide-group-item>
                    </v-slide-group>
                </v-col>
            </v-row>

            <v-row>
                <v-col cols="12">
                    <v-card>
                        <v-card-title>Уведомления
                            <v-spacer></v-spacer>
                            <v-text-field
                                v-model="search"
                                append-icon="mdi-magnify"
                                label="Search"
                                single-line
                                hide-details
                            ></v-text-field>
                        </v-card-title>
                        <VDataTable 
                            :headers="headers"
                            :items="subheaders"
                            :search="search"
                        ></VDataTable>
                    </v-card>
                </v-col>
            </v-row>
        </v-container>
    </v-form>
</template>

<script lang="ts">
import { VDataTable } from 'vuetify/labs/VDataTable'


export default {
    data: () => ({
      overlay: false,
    }),

    watch: {
      overlay (val) {
        val && setTimeout(() => {
          this.overlay = false
        }, 3000)
      },
    },

    components: {
    VDataTable,
 },
   
       
    }

</script>

<style>
/* Gradient */
.bg-green-gradient{
    background: linear-gradient(45deg, #e0f1ef, transparent);
}
.bg-blue-gradient{
    background: linear-gradient(45deg, #e4f2fd, transparent);
}
.bg-orange-gradient{
    background: linear-gradient(45deg, #feefe0, transparent);
}

.profile-user-icon{
    bottom: 18px;
}
.profile-user-data-box{
    display: inline-block;
}
.profile-user-name{
    font-size: 22px;
    line-height: 1;
    font-weight: 500;
}
.profile-user-login{
    font-size: 14px;
    color: #828181;
}

.profile-menu .v-btn-group--density-default.v-btn-group {
    height: 36px;
}
.profile-menu-item{
    background-color: #f6f7f8;
    border-radius: 15px!important;
    color: #999;
    width:150px;
}
.profile-menu-item.v-btn--active{
    
    background-color: #dfc8f7;
    color: #471f70;
}
.border-round {
    border-radius: 15px;
}

/* Slider */
.profile-slider-container{
    position:relative;
}
.profile-slider-item{
    padding: 10px 15px;
    width: 424px;
    margin: 20px 10px;
    height: 190px;
    display: flex;
    flex-direction: column;
}
.profile-slider-item-title{
    font-size: 18px;
    text-align: center;
    font-weight: 500;
}
.profile-slider-item-footer{
    flex-grow: 1;
}
.profile-slider-item-date{
    margin-left: 5px;
    font-size: 15px;
    color: #444444;
}
.profile-slider-item-date-icon{
    color: #444444;
    margin-bottom: 4px;
}
.profile-slider-item-link{
    background-color: #cce3f7;
    border: 1px solid #a4d7ff;
    border-radius: 10px;
    color: #2196F3;
    display: inline-block;
    float: right;
    font-size: 12px;
    padding: 4px 20px;
    text-decoration: none;
    transition: .3s;
}

.profile-slider-item-link:hover{
    background-color: #a5d5ff;
    border: 1px solid #72c0fd;
    color: #005aa2;
}

.v-slide-group__next{
    right: 10px;
}
.v-slide-group__prev{
    right: 90px;
}
.v-slide-group__next, .v-slide-group__prev{
    position: absolute;
    top: 1px;
    border: 1px solid #e9e9e9;
    border-radius: 50%;
    padding: 11px 0px;
    display: inherit;
    flex: inherit;
    background-color: #fff;
}
</style>