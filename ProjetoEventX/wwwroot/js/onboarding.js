/* ============================================
   EventX Onboarding & Guided Tour
   Powered by Driver.js (loaded via CDN)
   ============================================ */

(function () {
    'use strict';

    const STORAGE_KEY = 'eventx_onboarding_completed';
    const CHECKLIST_KEY = 'eventx_checklist';

    // ─────────────────────────────
    // Checklist System
    // ─────────────────────────────

    const defaultChecklist = {
        criarEvento: false,
        definirOrcamento: false,
        criarConvite: false,
        adicionarConvidados: false,
        solicitarOrcamento: false,
        explorarMarketplace: false,
        testarIA: false
    };

    const checklistMeta = {
        criarEvento: { label: 'Criar primeiro evento', icon: 'fas fa-calendar-plus', url: '/Eventos/Index' },
        definirOrcamento: { label: 'Definir orçamento do evento', icon: 'fas fa-coins', url: '/Organizador/Orcamento' },
        criarConvite: { label: 'Criar convite', icon: 'fas fa-envelope-open-text', url: '/TemplateConvite/Index' },
        adicionarConvidados: { label: 'Adicionar convidados', icon: 'fas fa-user-plus', url: '/Eventos/Index' },
        solicitarOrcamento: { label: 'Solicitar orçamento a fornecedores', icon: 'fas fa-file-invoice-dollar', url: '/Orcamento/Index' },
        explorarMarketplace: { label: 'Explorar o marketplace', icon: 'fas fa-store', url: '/Marketplace/Index' },
        testarIA: { label: 'Testar o Assistente IA', icon: 'fas fa-robot', url: '/IA/Index' }
    };

    function getChecklist() {
        try {
            const saved = localStorage.getItem(CHECKLIST_KEY);
            if (saved) return JSON.parse(saved);
        } catch (_) { /* ignore */ }
        return { ...defaultChecklist };
    }

    function saveChecklist(data) {
        localStorage.setItem(CHECKLIST_KEY, JSON.stringify(data));
    }

    function markChecklistItem(key) {
        const cl = getChecklist();
        if (cl.hasOwnProperty(key) && !cl[key]) {
            cl[key] = true;
            saveChecklist(cl);
            updateChecklistUI();
        }
    }

    function getChecklistProgress() {
        const cl = getChecklist();
        const keys = Object.keys(cl);
        const done = keys.filter(k => cl[k]).length;
        return { done, total: keys.length, percent: Math.round((done / keys.length) * 100) };
    }

    function updateChecklistUI() {
        const container = document.getElementById('eventx-checklist-card');
        if (!container) return;

        const cl = getChecklist();
        const progress = getChecklistProgress();

        if (progress.done === progress.total) {
            container.innerHTML = `
                <div class="saas-card-header">
                    <h5 class="saas-card-title"><i class="fas fa-trophy me-2" style="color: var(--eventx-warning);"></i>Primeiros Passos</h5>
                </div>
                <div class="text-center py-4">
                    <div style="font-size: 3rem; margin-bottom: 0.5rem;">🎉</div>
                    <h5 class="fw-bold mb-1" style="color: var(--eventx-success);">Parabéns!</h5>
                    <p class="text-muted mb-0">Você já configurou seu primeiro evento no EventX.</p>
                </div>
            `;
            return;
        }

        let itemsHtml = '';
        for (const [key, meta] of Object.entries(checklistMeta)) {
            const done = cl[key];
            itemsHtml += `
                <a href="${meta.url}" class="eventx-checklist-item ${done ? 'done' : ''}" data-key="${key}">
                    <span class="eventx-checklist-check">
                        <i class="${done ? 'fas fa-check-circle' : 'far fa-circle'}"></i>
                    </span>
                    <span class="eventx-checklist-icon"><i class="${meta.icon}"></i></span>
                    <span class="eventx-checklist-label">${meta.label}</span>
                    ${!done ? '<i class="fas fa-chevron-right ms-auto" style="font-size: 0.7rem; opacity: 0.4;"></i>' : ''}
                </a>
            `;
        }

        container.innerHTML = `
            <div class="saas-card-header">
                <h5 class="saas-card-title"><i class="fas fa-rocket me-2" style="color: var(--eventx-warning);"></i>Primeiros Passos</h5>
                <span class="badge" style="background: var(--eventx-primary); font-size: 0.75rem;">${progress.done}/${progress.total}</span>
            </div>
            <div class="eventx-checklist-progress">
                <div class="d-flex justify-content-between align-items-center mb-2">
                    <span style="font-size: 0.82rem; font-weight: 600; color: var(--eventx-text);">Progresso</span>
                    <span style="font-size: 0.82rem; font-weight: 700; color: var(--eventx-primary);">${progress.percent}%</span>
                </div>
                <div class="eventx-checklist-bar">
                    <div class="eventx-checklist-bar-fill" style="width: ${progress.percent}%"></div>
                </div>
            </div>
            <div class="eventx-checklist-items">${itemsHtml}</div>
        `;
    }

    // Auto-detect checklist completions based on current page
    function autoDetectChecklist() {
        const path = window.location.pathname.toLowerCase();
        const cl = getChecklist();
        let changed = false;

        // Detect marketplace visit
        if (path.includes('/marketplace') && !cl.explorarMarketplace) {
            cl.explorarMarketplace = true; changed = true;
        }
        // Detect IA visit
        if (path.includes('/ia') && !cl.testarIA) {
            cl.testarIA = true; changed = true;
        }

        if (changed) {
            saveChecklist(cl);
        }
    }

    // ─────────────────────────────
    // Tour System (Driver.js)
    // ─────────────────────────────

    function isOnboardingCompleted() {
        return localStorage.getItem(STORAGE_KEY) === 'true';
    }

    function setOnboardingCompleted() {
        localStorage.setItem(STORAGE_KEY, 'true');
    }

    function resetOnboarding() {
        localStorage.removeItem(STORAGE_KEY);
        localStorage.removeItem(CHECKLIST_KEY);
        startTour();
    }

    function startTour() {
        if (typeof window.driver === 'undefined' || !window.driver.js) {
            console.warn('Driver.js not loaded');
            return;
        }

        const { driver } = window.driver.js;

        const driverInstance = driver({
            showProgress: true,
            animate: true,
            overlayColor: 'rgba(15, 22, 33, 0.75)',
            stagePadding: 8,
            stageRadius: 12,
            popoverClass: 'eventx-tour-popover',
            progressText: 'Etapa {{current}} de {{total}}',
            nextBtnText: 'Próximo →',
            prevBtnText: '← Voltar',
            doneBtnText: 'Começar a usar o sistema',
            onDestroyed: () => {
                setOnboardingCompleted();
            },
            steps: buildTourSteps()
        });

        driverInstance.drive();
    }

    function buildTourSteps() {
        const steps = [];

        // Step 1: Welcome (no element highlight)
        steps.push({
            popover: {
                title: '👋 Bem-vindo ao EventX!',
                description: 'Vamos te mostrar rapidamente como organizar seus eventos de forma simples e eficiente.',
                side: 'over',
                align: 'center'
            }
        });

        // Step 2: Dashboard area
        const welcomeBanner = document.querySelector('.welcome-banner');
        if (welcomeBanner) {
            steps.push({
                element: '.welcome-banner',
                popover: {
                    title: '📊 Dashboard',
                    description: 'Aqui você acompanha os principais dados dos seus eventos, convidados, pedidos e despesas.',
                    side: 'bottom',
                    align: 'start'
                }
            });
        }

        // Step 3: Create Event - try Quick Actions first, then sidebar link
        const createEventBtn = document.querySelector('.quick-action[href*="CriarEvento"]') ||
            document.querySelector('a.sidebar-link[href*="Eventos"]');
        if (createEventBtn) {
            steps.push({
                element: createEventBtn,
                popover: {
                    title: '📅 Criar Evento',
                    description: 'Nesta área você pode criar um novo evento e iniciar todo o planejamento.',
                    side: 'bottom',
                    align: 'start'
                }
            });
        }

        // Step 4: Convites sidebar link
        const convitesLink = document.querySelector('a.sidebar-link[href*="TemplateConvite"]');
        if (convitesLink) {
            steps.push({
                element: convitesLink,
                popover: {
                    title: '✉️ Convites',
                    description: 'Aqui você cria e personaliza convites para enviar aos convidados.',
                    side: 'right',
                    align: 'start'
                }
            });
        }

        // Step 5: Orçamentos sidebar link
        const orcamentosLink = document.querySelector('a.sidebar-link[href*="Orcamento/Index"]') ||
            document.querySelector('a.sidebar-link[href*="Orcamento"]');
        if (orcamentosLink) {
            steps.push({
                element: orcamentosLink,
                popover: {
                    title: '💰 Orçamentos',
                    description: 'Nesta área você pode solicitar e acompanhar propostas de fornecedores.',
                    side: 'right',
                    align: 'start'
                }
            });
        }

        // Step 6: Marketplace sidebar link
        const marketplaceLink = document.querySelector('a.sidebar-link[href*="Marketplace"]');
        if (marketplaceLink) {
            steps.push({
                element: marketplaceLink,
                popover: {
                    title: '🏪 Marketplace',
                    description: 'Aqui você encontra fornecedores como buffet, decoração, fotografia e outros serviços.',
                    side: 'right',
                    align: 'start'
                }
            });
        }

        // Step 7: Assistente IA
        const iaLink = document.querySelector('a.sidebar-link[href*="/IA"]') ||
            document.querySelector('.floating-chat-btn');
        if (iaLink) {
            steps.push({
                element: iaLink,
                popover: {
                    title: '🤖 Assistente IA',
                    description: 'O Assistente IA pode ajudar a gerar planejamento, convites e estimativas de orçamento automaticamente.',
                    side: 'right',
                    align: 'start'
                }
            });
        }

        // Step 8: Finish
        steps.push({
            popover: {
                title: '🎉 Pronto!',
                description: 'Agora você já conhece as principais funcionalidades do EventX. Explore o sistema e comece a criar seus eventos!',
                side: 'over',
                align: 'center'
            }
        });

        return steps;
    }

    // ─────────────────────────────
    // Initialization
    // ─────────────────────────────

    function init() {
        // Only run for authenticated users with saas-layout
        if (!document.body.classList.contains('saas-layout')) return;

        // Auto-detect checklist items
        autoDetectChecklist();

        // Render checklist card if present
        updateChecklistUI();

        // Check if we should auto-start tour (first access on dashboard)
        const isDashboard = window.location.pathname.toLowerCase().includes('/organizador/dashboard') ||
            window.location.pathname.toLowerCase().includes('/fornecedor/dashboard');

        if (isDashboard && !isOnboardingCompleted()) {
            // Wait for the page to fully render, then start tour
            setTimeout(() => startTour(), 800);
        }
    }

    // Expose global functions
    window.EventXOnboarding = {
        startTour: startTour,
        resetOnboarding: resetOnboarding,
        markChecklistItem: markChecklistItem,
        getChecklistProgress: getChecklistProgress,
        updateChecklistUI: updateChecklistUI
    };

    // Initialize on DOMContentLoaded
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }
})();
