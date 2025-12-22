const ACTION_HIDE = 'hide'
const ACTION_SHOW = 'show'
let leftSidebarWidth, rightSidebarWidth

/*
 * Event handler
 */
window.addEventListener(
  'load',
  function (event) {

    document.onclick = hideMenu

    $('#button-browse').click(function () {
      $('#load_file').click()
    })

    $('#button-collapse-all').click(function () {
      collapseAllTreeItems()
    })

    $('#button-collapse-selected').click(function () {
      collapseSelectedTreeItems()
    })

    $('#button-hide-all').click(function () {
      hideAllTreeItems()
    })

    $('#button-hide-selected').click(function () {
      hideSelectedTreeItems()
    })

    $('#view-item').click(function (event) {
      handleViewItemClick(event)
    })

    $('#collapse-all-props').click(function () {
      toggleAllSets(PROPERTIES_CONTAINER, ACTION_HIDE)
    })

    $('#expand-all-props').click(function () {
      toggleAllSets(PROPERTIES_CONTAINER, ACTION_SHOW)
    })

    $('#collapse-all-rules').click(function () {
      toggleAllSets(RULES_CONTAINER, ACTION_HIDE)
    })

    $('#expand-all-rules').click(function () {
      toggleAllSets(RULES_CONTAINER, ACTION_SHOW)
    })

    leftSidebarWidth = $('#leftside').width()
    rightSidebarWidth = $('#rightside').width()
  },
  false
)

window.onresize = onWindowSizeHandler

function onWindowSizeHandler() {
  $('#rightside').css('left', 'auto')
  $('#rightside').css('right', '0')
}

const toggleAllSets = (container, action) => {
  const allSetsSelector = $(`${container} .collapse`)

  allSetsSelector.each(function () {
    $(this).collapse(action)
  })

  const allHeaderSelector = $(`${container} .arrow`)

  allHeaderSelector.each(function () {
    if (action === 'hide') {
      $(this).addClass('collapsed')
    } else {
      $(this).removeClass('collapsed')
    }
  })
}
