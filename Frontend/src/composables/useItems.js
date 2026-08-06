import { ref } from "vue";
import { createItem, getMyItems, deleteItem, patchItemStatus, putItem } from "../services/itemService";

const items = ref([]);
const loading = ref(false);
const query = ref({
  filter: "all",
  sort: "newest"
});

async function fetchItems() {
  items.value = await getMyItems(query.value);
}

async function setQuery(newQuery) {
  query.value = {
    ...query.value,
    ...newQuery
  };
  await fetchItems();
}

export function useItems() {

  async function execute(action) {
    if (loading.value) return;
    loading.value = true;

    try {
      return await action();
    } finally {
      loading.value = false;
    }
  }


  async function loadMyItems() {

    return execute(fetchItems);

  }

  async function create(item) {

    return execute(async () => {

      await createItem(item);
      await fetchItems();

    });
  }

  async function remove(id) {

    return execute(async () => {

      await deleteItem(id);
      await fetchItems();

    });
  }

  async function patch(id) {

    return execute(async () => {

      await patchItemStatus(id);
      await fetchItems();

    });
  }

  async function update(id, item) {

    return execute(async () => {

      await putItem(id, item);
      await fetchItems();

    });
  }

  return { loading, items, query, setQuery, create, loadMyItems, remove, patch, update };
}