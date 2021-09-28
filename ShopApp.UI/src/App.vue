<template>
  <div>
    <router-view></router-view>
  </div>
</template>

<script>
export default {
  name: "App",
  data() {
    return {
    };
  },
  created() {
    this.getUsers();
  },
  methods: {
    getUsers() {
      this.axios
        .get("https://mehmetakbudak.site/admin/user/list")
        .then((res) => {
          console.log(res.data);
        });
    },
  },
};
</script>

<style>
body {
  font-family: Avenir, Helvetica, Arial, sans-serif;
}
#app {
  color: #2c3e50;
}
.logo {
  width: 50%;
}
.btn {
  margin-top: 100px;
}
.ant-layout {
  background-color: white !important;
}
</style>
