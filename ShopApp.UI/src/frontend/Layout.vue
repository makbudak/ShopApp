<template>
    <a-layout class="layout">
    <a-layout-header>
      <div class="logo" />
      <a-menu
        theme="dark"
        mode="horizontal"
        v-model:selectedKeys="selectedKeys"
        :style="{ lineHeight: '64px' }"
      >
        <a-menu-item key="1"><router-link to="/">Anasayfa</router-link></a-menu-item>
        <a-menu-item key="2"><router-link to="/blog">Blog</router-link></a-menu-item>
        <a-menu-item key="3">nav 3</a-menu-item>
      </a-menu>
    </a-layout-header>
    <a-layout-content style="padding: 10px 50px; min-height: 200px;">
      <router-view></router-view>
    </a-layout-content>
    <a-layout-footer>
    ShopApp 
    </a-layout-footer>
  </a-layout>
</template>

<script>
export default {
data() {
    return {
        selectedKeys: []
    }
},
}
</script>

<style>

</style>