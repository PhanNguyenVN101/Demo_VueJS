<template>
  <div class="about">
    <h1>About Page</h1>
    <p>This is the about page content.</p>

    <!-- Animating with CSS transitions -->
    <button class="style-button" @click="toggleTransition">
      Toggle Transition
    </button>
    <div
      class="transition-box"
      :class="{ 'transition-active': transitionActive }"
    ></div>

    <!-- Fine-tuning Transitions -->
    <button class="my-style-button" @click="toggleFineTransition">
      Toggle Fine Transition
    </button>
    <div
      class="fine-transition-box"
      :class="{ 'fine-transition-active': fineTransitionActive }"
    ></div>

    <!-- Animating with CSS animations -->
    <div class="animation-box"></div>

    <!-- Animating with JavaScript -->
    <div
      class="javascript-box"
      ref="javascriptBox"
      @click="animateWithJS"
    ></div>

    <!-- JavaScript Zoom Animation -->
    <img
      :style="{ transform: 'scale(' + zoomLevel + ')' }"
      src="https://via.placeholder.com/150"
      @click="zoomIn"
    />

    <!-- Animating a List -->
    <ul>
      <li v-for="(item, index) in items" :key="index" class="list-item">
        {{ item }}
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  name: "AboutPage",
  data() {
    return {
      transitionActive: false,
      fineTransitionActive: false,
      zoomLevel: 1,
      items: ["Item 1", "Item 2", "Item 3"],
    };
  },
  methods: {
    toggleTransition() {
      this.transitionActive = !this.transitionActive;
    },
    toggleFineTransition() {
      this.fineTransitionActive = !this.fineTransitionActive;
    },
    animateWithJS() {
      const box = this.$refs.javascriptBox;
      box.style.animation = "slide 2s ease-in-out";
    },
    zoomIn() {
      this.zoomLevel += 0.1;
    },
  },
};
</script>

<style scoped lang="scss">
@import "@/assets/variables.scss";
@import "@/assets/mixins.scss";
@import "@/assets/base.scss";

/* Common styles */
.about {
  padding: 20px;
  background-color: #f0f0f0;
}

.highlight {
  background-color: yellow;
  color: black;
  border: 2px solid red;
}

h1 {
  color: $primary-color;
}

.style-button {
  @include button-styles($primary-color);
}

.my-style-button {
  @extend %button-base;
  background-color: $secondary-color;

  &:hover {
    opacity: 0.8;
  }
}

/* Animating with CSS transitions */
.transition-box {
  width: 100px;
  height: 100px;
  background-color: red;
  transition: width 2s, height 2s, transform 2s;
}

.transition-active {
  width: 200px;
  height: 200px;
  transform: rotate(180deg);
}

/* Fine-tuning Transitions */
.fine-transition-box {
  width: 100px;
  height: 100px;
  background-color: green;
  transition: width 0.5s ease-in-out, height 0.5s ease-in-out,
    background-color 0.5s ease-in-out;
}

.fine-transition-active {
  width: 150px;
  height: 150px;
  background-color: orange;
}

/* Animating with CSS animations */
.animation-box {
  width: 100px;
  height: 100px;
  background-color: blue;
  animation: bounce 2s infinite alternate;
}

@keyframes bounce {
  0% {
    transform: translateY(0);
  }
  100% {
    transform: translateY(-20px);
  }
}

/* JavaScript animation */
.javascript-box {
  width: 100px;
  height: 100px;
  background-color: pink;
}

@keyframes slide {
  0% {
    transform: translateX(0);
  }
  100% {
    transform: translateX(200px);
  }
}

/* Animating a List */
.list-item {
  transition: background-color 0.5s, transform 0.5s;
}

.list-item:hover {
  background-color: lightgray;
  transform: scale(1.1);
}
</style>
