<script>

</script>

<template>
  <div class="az-header">
    <div class="container">
      <div class="az-header-left">
        <a href="/" class="az-logo"><span></span> Apex</a>
        <a href="" id="azMenuShow" class="az-header-menu-icon d-lg-none"><span></span></a>
      </div><!-- az-header-left -->
      <div class="az-header-menu">
        <div class="az-header-menu-header">
          <a href="/" class="az-logo"><span></span> Apex</a>
          <a href="" class="close">&times;</a>
        </div><!-- az-header-menu-header -->
        <ul class="nav">
          <li class="nav-item">
            <router-link to="/" class="nav-link" exact exact-active-class="text-primary">
              <i class="typcn typcn-chart-area"></i> Dashboard
            </router-link>
          </li>
          <li class="nav-item">
            <router-link to="/banks" class="nav-link" exact exact-active-class="text-primary">
              <i class="typcn typcn-chart-bar"></i> Banks
            </router-link>
          </li>
          <li class="nav-item">
            <router-link to="/packs" class="nav-link" exact exact-active-class="text-primary">
              <i class="typcn typcn-home"></i> Packs
            </router-link>
          </li>
          <li class="nav-item">
            <router-link to="/returns" class="nav-link" exact exact-active-class="text-primary">
              <i class="typcn typcn-arrow-down-thick"></i> Returns
            </router-link>
          </li>
          <li class="nav-item">
            <router-link to="/returns" class="nav-link" exact exact-active-class="text-primary">
              <i class="typcn typcn-archive"></i> Generated Returns
            </router-link>
          </li>
        </ul>
      </div><!-- az-header-menu -->
      <div class="az-header-right">
        <div class="dropdown az-profile-menu">
          <a href="" class="az-img-user">
            <img src="/apex-icons/icon1.png" alt="">
          </a>
          <div class="dropdown-menu">
            <div class="az-dropdown-header d-sm-none">
              <a href="" class="az-header-arrow"><i class="icon ion-md-arrow-back"></i></a>
            </div>
            <div class="az-header-profile">
              <div class="az-img-user">
                <img src="/apex-icons/icon1.png" alt="">
              </div>
              <h6>Aziana Pechon</h6>
              <span>Premium Member</span>
            </div>

            <a href="#" class="dropdown-item"><i class="typcn typcn-user-outline"></i> My Profile</a>
            <a href="#" class="dropdown-item"><i class="typcn typcn-cog-outline"></i> Account Settings</a>
            <a href="#" class="dropdown-item"><i class="typcn typcn-power-outline"></i> Sign Out</a>
          </div><!-- dropdown-menu -->
        </div>
      </div><!-- az-header-right -->
    </div><!-- container -->
  </div>
</template>